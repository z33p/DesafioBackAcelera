using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DesafioBack.Contracts.Responses.Shared;
using DesafioBack.Models;

namespace DesafioBack.Contracts.Responses
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
            , Duration = (int) Math.Ceiling(XmlConvert.ToTimeSpan(this.ContentDetails.Duration).TotalSeconds)
            , PublishedAt = this.Snippet.PublishedAt
        };

        public static List<Video> toEntity(List<GetUrlVideosContentResponse> videosReponse)
        {
            return videosReponse.Select(v => v.toEntity()).ToList();
        }
    }

}