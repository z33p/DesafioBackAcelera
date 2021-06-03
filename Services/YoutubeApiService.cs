using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DesafioBack.Contracts;
using DesafioBack.Contracts.Responses.Youtube;
using DesafioBack.Contracts.Responses.Youtube.Shared;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Services.Shared;

namespace DesafioBack.Services
{
    public class YoutubeApiService : ServiceAbstract, IYoutubeApiService
    {
        public readonly HttpClient client = new HttpClient();

        private readonly IYoutubeApiRoutes _youtubeApiRoutes;

        public YoutubeApiService(IRepository repository, IYoutubeApiRoutes youtubeApiRoutes) : base(repository)
        {
            _youtubeApiRoutes = youtubeApiRoutes;
        }

        public async Task SeedDatabase()
        {
            var videosIds = await GetYoutubeVideosIds();

            var url = _youtubeApiRoutes.GetUrlVideosContent(videosIds);

            var res = await client.GetAsync(url);

            var contentBody = await res.Content.ReadAsStringAsync();
            
            var youtubeResponse = await res.Content.ReadFromJsonAsync<YoutubeItemsResponse<GetUrlVideosContentResponse>>();

            var videos = GetUrlVideosContentResponse.toEntity(youtubeResponse.Items);

            await repository.Insert(videos);
        }

        private async Task<List<string>> GetYoutubeVideosIds()
        {
            var url = _youtubeApiRoutes.GetUrlSearchVideosIds(
                q: "manipulação"
                , regionCode: "BR"
                , publishedAfter: new DateTime(2020, 1, 1)
                , publishedBefore: new DateTime(2021, 1, 1)
            );

            var res = await client.GetAsync(url);

            var contentBody = await res.Content.ReadAsStringAsync();
            
            var youtubeResponse = await res.Content.ReadFromJsonAsync<YoutubeItemsResponse<GetUrlSearchVideosIdsResponse>>();

            return youtubeResponse.Items.Select(v => v.Id.VideoId).ToList();
        }
    }
}
