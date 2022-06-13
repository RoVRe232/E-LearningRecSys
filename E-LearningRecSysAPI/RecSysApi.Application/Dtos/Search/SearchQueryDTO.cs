using RecSysApi.Application.Dtos.Pagination;
using System.Collections.Generic;

namespace RecSysApi.Application.Dtos.Search
{
    public class SearchQueryDTO
    {
        public List<string> KeyPhrases
        {
            get; set;
        }
        public List<FilterDTO> Filters
        {
            get; set;
        }
        public PaginationOptionsDTO PaginationOptions
        {
            get; set;
        }
    }
}
