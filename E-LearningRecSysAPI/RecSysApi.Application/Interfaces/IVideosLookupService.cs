using RecSysApi.Application.Dtos;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Models;
using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IVideosLookupService
    {
        Task<ICollection<Video>> LookupForVideosAsync(GetVideosQueryDto getVideosQueryDto);
    }
}
