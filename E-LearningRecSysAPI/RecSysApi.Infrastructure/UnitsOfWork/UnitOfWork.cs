using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using RecSysApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IVideoRepository _videoRepository;
        private IQueryRepository _queryRepository;

        public UnitOfWork(RecSysApiContext dbContext)
        {
            _videoRepository = new VideoRepository(dbContext);
            _queryRepository = new QueryRepository(dbContext);
        }

        public void Save()
        {
            _videoRepository.Save();
        }
    }
}
