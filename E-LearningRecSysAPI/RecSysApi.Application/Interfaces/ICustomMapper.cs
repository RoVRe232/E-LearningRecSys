using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface ICustomMapper<TSource, TDestination>
    {
        public TDestination Map(TSource sourceDto);
    }
}
