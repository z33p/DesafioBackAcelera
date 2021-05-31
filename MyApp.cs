using System.Threading.Tasks;
using DesafioBack.Data;
using DesafioBack.Services;

namespace DesafioBack
{
    public class MyApp
    {
        public async static Task Init()
        {
            await MyDatabase.Instance.InitDatabase();
            await YoutubeApiService.Instance.SeedDatabase();
        } 
    }
}