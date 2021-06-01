using System;
using DesafioBack.Services.Videos.Filters;

namespace DesafioBack.Contracts.Requests
{
    public class GetVideosRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Duration { get; set; }
        public DateTime? PublishedAfter { get; set; }

        public VideoFilterQueryBuilder ToQueryBuilder()
        {
            var filterBuilder = new VideoFilterQueryBuilder()
                .SetTitle(this.Title)
                .SetAuthor(this.Author);

            if (this.PublishedAfter != null)
                filterBuilder.SetPublishedAfter(this.PublishedAfter.Value);

            if (!string.IsNullOrWhiteSpace(this.Duration))
                filterBuilder.SetDuration(this.Duration);

            return filterBuilder;
        }
    }
}