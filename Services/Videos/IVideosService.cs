using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioBack.Models;
using DesafioBack.Services.Videos.Filters;

namespace DesafioBack.Services.Videos
{
    public interface IVideosService
    {
        Task<List<Video>> GetVideos(VideoFilterQueryBuilder filterBuilder);
        Task<long> CreateVideo(Video video);
        Task UpdateVideo(Video video);
        Task DeleteVideo(long id);
    }
}