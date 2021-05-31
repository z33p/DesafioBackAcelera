using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DesafioBack.Contracts;
using DesafioBack.Contracts.Responses;
using DesafioBack.Contracts.Responses.Shared;
using DesafioBack.Services.Shared;

namespace DesafioBack.Services
{
    public class YoutubeApiService : ServiceAbstract
    {
        private static YoutubeApiService _instance = new YoutubeApiService();
        public static YoutubeApiService Instance { get => _instance; }

        public readonly HttpClient client = new HttpClient();

        public async Task SeedDatabase()
        {
            var videosIds = await GetYoutubeVideosIds();

            var url = YoutubeApiRoutes.GetUrlVideosContent(videosIds);

            var res = await client.GetAsync(url);

            var contentBody = await res.Content.ReadAsStringAsync();
            
            var youtubeResponse = await res.Content.ReadFromJsonAsync<YoutubeItemsResponse<GetUrlVideosContentResponse>>();

            var videos = GetUrlVideosContentResponse.toEntity(youtubeResponse.Items);

            await Repository.Insert(videos);
        }

        private async Task<List<string>> GetYoutubeVideosIds()
        {
            var url = YoutubeApiRoutes.GetUrlSearchVideosIds(q: "manipulação", regionCode: "BR");

            var res = await client.GetAsync(url);

            var contentBody = await res.Content.ReadAsStringAsync();
            
            var youtubeResponse = await res.Content.ReadFromJsonAsync<YoutubeItemsResponse<GetUrlSearchVideosIdsResponse>>();

            return youtubeResponse.Items.Select(v => v.Id.VideoId).ToList();
        }
    }
}
