using System.Collections.Generic;
using DesafioBack.Data.Repositories.shared;

namespace DesafioBack.Data.Shared
{
    public interface ISqlSnippets
    {
        string ColumnsSql(List<string> columns);
        string Insert(string tableName, Dictionary<string, dynamic> entity);
        string Insert(string tableName, List<Dictionary<string, dynamic>> entities);
        string OrderBySql(string orderByColumn, int? limit = null, int? offset = null);
        string WhereColumn(string columnName, dynamic value, ComparationSymbol symbolEnum = ComparationSymbol.EQUAL);
        string WhereColumnContains(string columnName, string value);
        string WheresSql(List<string> sqlWheresList);
        string GetLastInsertedRowId();
    }
}