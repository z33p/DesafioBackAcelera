using System.Collections.Generic;

namespace DesafioBack.Models.Shared
{
    public interface IEntity<E> where E : IEntity<E>
    {
        IDbTable<E> DbTable { get; }
    }
}