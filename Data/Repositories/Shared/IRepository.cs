using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioBack.Models.Shared;
using DesafioBack.Models.Tables;

namespace DesafioBack.Data.Repositories.shared
{
    public interface IRepository
    {
        Task<List<E>> FindByQuery<E>(string rawQuery) where E : IEntity<E>, new();
        Task<long> Insert<E>(E entity) where E : IEntity<E>;
        Task Insert<E>(List<E> entities) where E : IEntity<E>;
        Task Update<E>(E entity) where E : IEntity<E>;
        Task UpdateWherePkEquals<E>(long id, IDbTable<E> instance, Dictionary<string, dynamic> columns) where E : IEntity<E>;
    }
}