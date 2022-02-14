using System.Collections.Generic;

namespace hafta1WebApi.Models
{
    public class PagingAndFilterResponseModel
    {
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; } = 1;
        public int CurrentPage { get; set; } = 1;
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }   

    



    }
}
