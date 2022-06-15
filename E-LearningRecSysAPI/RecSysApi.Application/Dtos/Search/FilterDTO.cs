using RecSysApi.Application.Dtos.Enums;

namespace RecSysApi.Application.Dtos.Search
{
    public class FilterDTO
    {
        public FilterType Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public FilterDTO[]? SubFilters { get; set; }
    }
}
