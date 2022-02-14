using hafta1WebApi.DBOperations;
using hafta1WebApi.Models;
using System.Collections.Generic;

namespace hafta1WebApi.Repository
{
    public interface IProductRepository
    {
        PagingAndFilterResultModel<Product> GetProducts(QueryParams queryParams);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext context)
        {
            _context = context;

        }
        public PagingAndFilterResultModel<Product> GetProducts(QueryParams queryParams)
        {
            PagingAndFilterResultModel<Product> products = new PagingAndFilterResultModel<Product>(queryParams);

            products.GetData(_context.Products);

            return products;
        }
    }
}
