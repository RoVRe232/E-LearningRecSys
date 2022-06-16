using RecSysApi.Application.Dtos.Enums;
using System.Collections.Generic;

namespace RecSysApi.Application.Dtos.Search
{
    public class FilterDTO
    {
        public FilterType Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; } = false;
        public string Color { get; set; } = "primary";
        public double? LowerBound { get; set; }
        public double? UpperBound { get; set; }
        public double? LowValue { get; set; }
        public double? HighValue { get; set; }
        public List<FilterDTO> SubFilters { get; set; }
    }
}
