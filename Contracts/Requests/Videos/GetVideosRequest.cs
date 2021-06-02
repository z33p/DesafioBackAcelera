using System;
using DesafioBack.Data.Shared;
using DesafioBack.Services.Videos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBack.Contracts.Requests.Videos
{
    public class GetVideosRequest
    {
        private readonly ISqlSnippets _sqlSnippets;

        public GetVideosRequest(ISqlSnippets sqlSnippets)
        {
            _sqlSnippets = sqlSnippets;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Duration { get; set; }
        public DateTime? PublishedAfter { get; set; }

        public VideoFilterQueryBuilder ToQueryBuilder()
        {
            var filterBuilder = new VideoFilterQueryBuilder(_sqlSnippets)
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