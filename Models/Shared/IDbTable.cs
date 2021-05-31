using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace DesafioBack.Models.Shared
{
    public interface IDbTable<E> where E : IEntity<E>
    {
        string TableName { get; set; }
        
        Task CreateTable(SQLiteConnection connection);

        Dictionary<string, dynamic> EntityMapToDatabase(E entity);

        List<Dictionary<string, dynamic>> EntityMapToDatabase(List<E> entities);
    }
}