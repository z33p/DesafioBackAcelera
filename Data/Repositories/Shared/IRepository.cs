using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioBack.Models.Shared;

namespace DesafioBack.Data.Repositories.shared
{
    public interface IRepository
    {
        Task<List<E>> FindByQuery<E>(string rawQuery) where E : IEntity<E>, new();
        Task<string> Insert<E>(E entity) where E : IEntity<E>;
        Task Insert<E>(List<E> entities) where E : IEntity<E>;
    }
}