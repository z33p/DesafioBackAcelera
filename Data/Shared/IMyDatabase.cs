using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioBack.Data.Shared
{
    public interface IMyDatabase
    {
        Task ExecuteNonQueryAsync(string sql);
        Task InitDatabase();
        Task<object> ExecuteScalarAsync(string sql);
        Task<List<Dictionary<string, dynamic>>> ExecuteQueryAsync(string sql);
    }
}