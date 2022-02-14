using hafta1WebApi.DBOperations;
using hafta1WebApi.Models;
using hafta1WebApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hafta1WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedcache;
        private readonly ProductDbContext _context;

        public ProductController(IProductRepository productRepository, IMemoryCache memoryCache, IDistributedCache distributedCache, ProductDbContext context)
        {
            _productRepository = productRepository;
            _memoryCache = memoryCache;
            _distributedcache = distributedCache;
            _context = context;
        }
        [HttpGet]
        public IActionResult Get([FromQuery]Models.QueryParams query)
        {
            if(_memoryCache.TryGetValue("products", out List<Product> products) && _memoryCache.TryGetValue("filters", out List<QueryParams> queries))
            {
                return Ok(new {data= products ,filtre = queries});
            }

            
            var list = _productRepository.GetProducts(query);
            _memoryCache.Set("products", list, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
                
            }) ;
            _memoryCache.Set("filters", query, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)

            });
            Response.Headers.Add("X-Paging", System.Text.Json.JsonSerializer.Serialize(list.Result));

            object items = null;
            if (query.Search != null)
            {
                items = list.FindAll(x => x.Name.Contains(query.Search));
            }
            else
            {
                items = list;
            }
            //Bir extension method yazmak daha mantıklı
            //if (query.StockMin != null)
            //{
            //    list.FindAll(x => x.Stock < query.StockMin.Value);
            //}
            //if (query.PriceMax != null)
            //{
            //    list.FindAll(x => x.Price < query.PriceMax);
            //}





            return Ok(new { data= items, paging = list.Result});  
        }
        [HttpGet("Cache")]
        public ActionResult<List<Product>>  GetAll()
        {
            var cachedData = _distributedcache.GetString("allProducts");
            
            
            if( string.IsNullOrEmpty(cachedData))
            {
                var products = _context.Products.ToList();
                var cacheOptions = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                };
                _distributedcache.SetString("allProducts", JsonConvert.SerializeObject(products), cacheOptions);
                return products;
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Product>>(cachedData);
            }

            

           
        }
    }
}
