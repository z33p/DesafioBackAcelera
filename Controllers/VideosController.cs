using System;
using System.Threading.Tasks;
using DesafioBack.Contracts.Requests.Videos;
using DesafioBack.Contracts.Responses.Shared;
using DesafioBack.Contracts.Responses.Videos;
using DesafioBack.Contracts.Responses.Videos.Shared;
using DesafioBack.Data.Shared;
using DesafioBack.Services.Videos;
using Microsoft.AspNetCore.Http;
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

        public async Task<ActionResult> GetVideos(string title, string author, string duration, DateTime? publishedAfter)
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

            return Ok(new PayloadResponse<VideoResponse>(response));
        }

        [HttpPost]
        public async Task<ActionResult> CreateVideo([FromBody] CreateVideoRequest createVideoRequest)
        {
            var video = createVideoRequest.ToEntity();

            video.Id = await _videosService.CreateVideo(video);

            return new ObjectResult(new CreateVideoResponse(video.Id)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVideo([FromBody] UpdateVideoRequest UpdateVideoRequest)
        {
            var video = UpdateVideoRequest.ToEntity();

            await _videosService.UpdateVideo(video);

            return NoContent();
        }
    }
}
