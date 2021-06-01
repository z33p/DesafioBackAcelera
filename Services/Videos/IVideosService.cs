using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioBack.Models;
using DesafioBack.Services.Videos.Filters;

namespace DesafioBack.Services.Videos
{
    public interface IVideosService
    {
        Task<List<Video>> GetVideos(VideoFilterQueryBuilder filterBuilder);
        Task<string> CreateVideo(Video video);
    }
}