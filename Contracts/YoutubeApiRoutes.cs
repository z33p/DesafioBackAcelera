using System;
using System.Collections.Generic;
using DesafioBack.Config;
using Microsoft.Extensions.Options;

namespace DesafioBack.Contracts
{
    public class YoutubeApiRoutes : IYoutubeApiRoutes
    {
        private readonly AppSettings _appSettings;
        private readonly string Key;

        public YoutubeApiRoutes(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            Key = _appSettings.Key;
        }

        public const string BaseUrl = "https://www.googleapis.com/youtube/v3";
        public const string SearchBaseUrl = BaseUrl + "/search";
        public const string VideosBaseUrl = BaseUrl + "/videos";

        private const string UrlParameterMaxPerPage = "maxResults=50";

        public string GetUrlSearchVideosIds(string q, string regionCode, DateTime publishedAfter, DateTime publishedBefore)
        {
            var url = $"{SearchBaseUrl}?part=Id&q={q}&regionCode={regionCode}&{UrlParameterMaxPerPage}"; 
            url += $"&publishedAfter={publishedAfter.ToString("yyyy-MM-ddTHH:mm:ssZ")}&publishedBefore={publishedBefore.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
            url += $"&key={Key}";

            return url;
        }

        public string GetUrlVideosContent(List<string> videosIdList)
        {
            var url = $"{VideosBaseUrl}?id={string.Join(",", videosIdList)}&part=snippet,contentDetails&{UrlParameterMaxPerPage}&key={Key}"; 

            return url;
        }
    }
}