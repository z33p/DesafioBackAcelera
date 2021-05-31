using System.Collections.Generic;

namespace DesafioBack.Contracts.Responses.Shared
{
    public class YoutubeItemsResponse<T>
    {
        public List<T> Items { get; set; }
    }
}