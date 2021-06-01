using System;
using System.Threading.Tasks;
using DesafioBack.Contracts.Requests.Videos;
using DesafioBack.Contracts.Responses.Shared;
using DesafioBack.Contracts.Responses.Videos.Shared;
using DesafioBack.Data.Shared;
using DesafioBack.Services.Videos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesafioBackAcelera.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly ILogger<VideosController> _logger;
        private readonly IVideosService _videosService;
        private readonly ISqlSnippets _sqlSnippets;

        public VideosController(ILogger<VideosController> logger, IVideosService videosService, ISqlSnippets sqlSnippets)
        {
            _logger = logger;
            _videosService = videosService;
            _sqlSnippets = sqlSnippets;
        }

        public async Task<PayloadResponse<VideoResponse>> GetVideos(string title, string author, string duration, DateTime? publishedAfter)
        {
            var req = new GetVideosRequest(_sqlSnippets)
            {
                Title = title
                , Author = author
                , Duration = duration
                , PublishedAfter = publishedAfter
            };

            var videos = await _videosService.GetVideos(req.ToQueryBuilder());

            var response = VideoResponse.FromEntity(videos);

            return new PayloadResponse<VideoResponse>(response);
        }
    }
}
