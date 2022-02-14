using System;
using System.Collections.Generic;
using System.Linq;

namespace hafta1WebApi.Models
{
    public class PagingAndFilterResultModel<Product> : List<Product>
    {
        public QueryParams Params { get;  }
        public PagingAndFilterResponseModel Result { get; set; }

        public PagingAndFilterResultModel(QueryParams query)
        {
            Params = query;
            Result = new PagingAndFilterResponseModel();
        }

        public void GetData(IQueryable<Product> query)
        {
            Result.TotalCount = query.Count();
            Result.TotalPages = (int)Math.Ceiling(Result.TotalCount / (double)Params.PageSize);
            Result.CurrentPage = Params.Page;
            Result.NextPage = Result.CurrentPage +1 <= Result.TotalPages? Result.CurrentPage +1 : Result.CurrentPage;
            Result.PreviousPage = Result.CurrentPage == 1? Result.CurrentPage : Result.CurrentPage -1;

            var result = query.Skip((Params.Page - 1) * Params.PageSize).Take(Params.PageSize).ToList();

            if (string.IsNullOrWhiteSpace(Params.OrderType))
            {
                var entity = typeof(Product);

                var property = entity.GetProperty(Params.OrderType);
                //Noralde decs listeliyor çünkü

                result = result.OrderBy(x => property.GetValue(x, null)).ToList();



            }
            
            AddRange(result);

        }
    }
}
