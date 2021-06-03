using System;
using System.Collections.Generic;

namespace DesafioBack.Contracts
{
    public interface IYoutubeApiRoutes
    {
        string GetUrlSearchVideosIds(string q, string regionCode, DateTime publishedAfter, DateTime publishedBefore);
        string GetUrlVideosContent(List<string> videosIdList);
    }
}