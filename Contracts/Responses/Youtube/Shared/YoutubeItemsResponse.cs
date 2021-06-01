using System.Collections.Generic;

namespace DesafioBack.Contracts.Responses.Youtube.Shared
{
    public class YoutubeItemsResponse<T>
    {
        public List<T> Items { get; set; }
    }
}