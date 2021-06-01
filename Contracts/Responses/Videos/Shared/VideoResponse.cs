using System;
using System.Collections.Generic;
using System.Linq;
using DesafioBack.Contracts.Responses.Shared;
using DesafioBack.Models;

namespace DesafioBack.Contracts.Responses.Videos.Shared
{
    public class VideoResponse : IResponse
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Duration { get; set; }
        public DateTime PublishedAt { get; set; }

        public static VideoResponse FromEntity(Video video) => new VideoResponse
        {
            Title = video.Title
            , Author = video.Author
            , Duration = video.Duration
            , PublishedAt = video.PublishedAt
        };

        public static List<VideoResponse> FromEntity(List<Video> videos)
        {
            return videos.Select(v => FromEntity(v)).ToList();
        }
    }
}