using System;
using System.Collections.Generic;

namespace DesafioBack.Contracts
{
    public static class YoutubeApiRoutes
    {
        public const string Key = "AIzaSyBmIj5HqGIONtXrueRAwsZ39cDOLkvJVB4";
        public const string BaseUrl = "https://www.googleapis.com/youtube/v3";
        public const string SearchBaseUrl = BaseUrl + "/search";
        public const string VideosBaseUrl = BaseUrl + "/videos";

        private const string UrlParameterMaxPerPage = "maxResults=50";

        public static string GetUrlSearchVideosIds(string q, string regionCode, DateTime publishedAfter, DateTime publishedBefore)
        {
            var url = $"{SearchBaseUrl}?part=Id&q={q}&regionCode={regionCode}&{UrlParameterMaxPerPage}"; 
            url += $"&publishedAfter={publishedAfter.ToString("yyyy-MM-ddTHH:mm:ssZ")}&publishedBefore={publishedBefore.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
            url += $"&key={YoutubeApiRoutes.Key}";

            return url;
        }

        public static string GetUrlVideosContent(List<string> videosIdList)
        {
            var url = $"{VideosBaseUrl}?id={string.Join(",", videosIdList)}&part=snippet,contentDetails&{UrlParameterMaxPerPage}&key={YoutubeApiRoutes.Key}"; 

            return url;
        }
    }
}