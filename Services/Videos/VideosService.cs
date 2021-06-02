using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Models;
using DesafioBack.Services.Shared;
using DesafioBack.Services.Videos;
using DesafioBack.Services.Videos.Filters;

namespace DesafioBack.Services
{
    public class VideosService : ServiceAbstract, IVideosService
    {
        public VideosService(IRepository repository) : base(repository) {}

        public async Task<List<Video>> GetVideos(VideoFilterQueryBuilder filterBuilder)
        {
            var rawQuery = filterBuilder.BuildRawQuery();

            var videos = await repository.FindByQuery<Video>(rawQuery);   

            return videos;
        }

        public static string ConvertTicksToDuration(long duration)
        {
            return XmlConvert.ToString(new TimeSpan(duration));
        }

        public static long ConvertDurationToTicks(string duration)
        {
            if (string.IsNullOrWhiteSpace(duration))
                throw new Exception("The duration can't be null or whitespace");

            return XmlConvert.ToTimeSpan(duration).Ticks;
        }

        public async Task<long> CreateVideo(Video video)
        {
            return await repository.Insert<Video>(video);
        }
    }
}