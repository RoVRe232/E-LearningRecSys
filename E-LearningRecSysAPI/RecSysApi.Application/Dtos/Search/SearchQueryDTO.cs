using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Search
{
    public class SearchQueryDTO
    {
        public List<string> KeyPhrases { get; set; }
        public QueryFiltersDTO Filters { get; set; }
        public PaginationOptionsDTO PaginationOptions { get; set; }
    }
}
