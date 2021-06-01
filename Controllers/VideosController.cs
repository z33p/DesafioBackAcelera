using System;
using System.Threading.Tasks;
using DesafioBack.Contracts.Requests;
using DesafioBack.Contracts.Responses.Shared;
using DesafioBack.Contracts.Responses.Videos.Shared;
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

        public VideosController(ILogger<VideosController> logger, IVideosService videosService)
        {
            _logger = logger;
            _videosService = videosService;
        }

        public async Task<PayloadResponse<VideoResponse>> GetVideos(string title, string author, string duration, DateTime? publishedAfter)
        {
            var req = new GetVideosRequest
            {
                Title = title
                , Author = author
                , Duration = duration
                , PublishedAfter = publishedAfter
            };

            _logger.LogInformation(req.Title);

            var videos = await _videosService.GetVideos(req.ToQueryBuilder());

            var response = VideoResponse.FromEntity(videos);

            return new PayloadResponse<VideoResponse>(response);
        }
    }
}
