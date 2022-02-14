namespace hafta1WebApi.Models
{
    public class QueryParams
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string OrderType { get; set; } = "desc";

        public string? Search { get; set; }

        public int? PriceMax { get; set; }
        public int? StockMin { get; set; }
    }
}
