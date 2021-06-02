using System.Collections.Generic;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Models;
using DesafioBack.Models.Shared;

namespace DesafioBack.Data.Shared
{
    public interface ISqlSnippets
    {
        string ColumnsSql(List<string> columns);
        string Insert<E>(E entity) where E : IEntity<E>;
        string Insert<E>(List<E> entities) where E : IEntity<E>;
        string OrderBySql(string orderByColumn, int? limit = null, int? offset = null);
        string WhereColumn(string columnName, dynamic value, ComparationSymbol symbolEnum = ComparationSymbol.EQUAL);
        string WhereColumnContains(string columnName, string value);
        string WheresSql(List<string> sqlWheresList);
        string GetLastInsertedRowId();
        string Update<E>(E entity) where E : IEntity<E>;
    }
}