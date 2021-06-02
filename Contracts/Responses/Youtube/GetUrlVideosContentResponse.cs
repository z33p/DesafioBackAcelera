using System.Collections.Generic;
using System.Linq;
using DesafioBack.Contracts.Responses.Youtube.Shared;
using DesafioBack.Models;
using DesafioBack.Services;

namespace DesafioBack.Contracts.Responses.Youtube
{
    public class GetUrlVideosContentResponse
    {
        public string Id { get; set; }
        public Snippet Snippet { get; set; }
        public ContentDetails ContentDetails { get; set; }

        public Video toEntity() => new Video
        {
            VideoId = this.Id
            , Title = this.Snippet.Title
            , Author = this.Snippet.ChannelTitle
            , Duration = VideosService.ConvertDurationToTicks(this.ContentDetails.Duration)
            , PublishedAt = this.Snippet.PublishedAt
        };

        public static List<Video> toEntity(List<GetUrlVideosContentResponse> videosReponse)
        {
            return videosReponse.Select(v => v.toEntity()).ToList();
        }
    }
}