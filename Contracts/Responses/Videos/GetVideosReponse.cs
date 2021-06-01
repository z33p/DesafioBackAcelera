using System.Collections.Generic;
using DesafioBack.Contracts.Responses.Videos.Shared;

namespace DesafioBack.Contracts.Responses.Videos
{
    public class GetVideosResponse
    {
        public List<VideoResponse> Payload { get; set; }
    }
}