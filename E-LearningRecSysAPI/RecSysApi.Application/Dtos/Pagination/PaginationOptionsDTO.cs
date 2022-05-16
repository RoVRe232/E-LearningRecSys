using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Pagination
{
    public class PaginationOptionsDTO
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
