using System;

namespace DesafioBack.Contracts.Responses.Youtube.Shared
{
    public class Snippet
    {
        public string Title { get; set; }
        public string ChannelTitle { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}