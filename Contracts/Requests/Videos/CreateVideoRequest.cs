using System;
using System.ComponentModel.DataAnnotations;
using DesafioBack.Models;
using DesafioBack.Services;

namespace DesafioBack.Contracts.Requests.Videos
{
    public class CreateVideoRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public DateTime? PublishedAt { get; set; }

        public Video ToEntity() => new Video
        {
            Title = this.Title
            , Author = this.Author
            , Duration = VideosService.ConvertDurationToTicks(this.Duration)
            , PublishedAt = this.PublishedAt.Value
        };
    }
}