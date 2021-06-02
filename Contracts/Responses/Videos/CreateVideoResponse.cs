namespace DesafioBack.Contracts.Responses.Videos
{
    public class CreateVideoResponse
    {
        public long VideoId { get; set; }

        public CreateVideoResponse(long id)
        {
            VideoId = id;
        }
    }
}