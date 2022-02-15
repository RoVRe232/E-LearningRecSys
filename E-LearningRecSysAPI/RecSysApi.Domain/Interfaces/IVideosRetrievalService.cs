using RecSysApi.Domain.Commons.Models;
using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Interfaces
{
    public interface IVideosRetrievalService
    {
        Task<ICollection<VideoIdentifier>> GetVideosIdentifiersForQueryAsync(Query queryString);
        Task<ICollection<Video>> GetInternalVideosForQueryAsync(ICollection<VideoIdentifier> videosIdentifiers);
    }
}
