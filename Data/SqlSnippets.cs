using System;
using System.Collections.Generic;
using System.Linq;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Data.Shared;
using DesafioBack.Models.Shared;

namespace DesafioBack.Data
{
    public class SqlSnippets : ISqlSnippets
    {
        public string Insert<E>(E entity) where E : IEntity<E>
        {
            var entityDict = entity.DbTable.EntityMapToDatabase(entity);

            var values = GetValueColumnsFromEntity(entityDict);

            var sql = $@"
                INSERT INTO {entity.DbTable.TableName} ({string.Join(",", entityDict.Keys)})
                VALUES {values};
            ";

            return sql;
        }

        public string Insert<E>(List<E> entities) where E : IEntity<E>
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException($"[entities] is null or empty");

            var firstEntity = entities.First();

            var values = string.Join("," , entities.Select(entity => GetValueColumnsFromEntity(entity)));


            var sql = $@"
                INSERT INTO {firstEntity.DbTable.TableName} (
                    {string.Join(",", firstEntity.DbTable.EntityMapToDatabase(firstEntity).Keys)}
                )
                VALUES {values};
            ";
            
            return sql;
        }

        private string GetValueColumnsFromEntity(Dictionary<string, dynamic> entity)
        {
            return $"({string.Join(",", entity.Values.Select(AddQuotesIfNotNumericOrConvertIfBool))})";
        }

        private string GetValueColumnsFromEntity<E>(E entity) where E : IEntity<E>
        {
            var entityDict = entity.DbTable.EntityMapToDatabase(entity);
            return $"({string.Join(",", entityDict.Values.Select(AddQuotesIfNotNumericOrConvertIfBool))})";
        }

        private string AddQuotesIfNotNumericOrConvertIfBool(dynamic value)
        {
            if (value is bool?) return (value == true ? 1 : 0).ToString();

            double _;
            var isNumeric = double.TryParse(value.ToString(), out _);

            if (isNumeric) return $"{value}";

            return $"'{value}'";
        }

        public string WheresSql(List<string> sqlWheresList)
        {
            var sqlWheres = "";

            sqlWheresList = sqlWheresList.Where(where => !string.IsNullOrEmpty(where)).ToList();

            if (sqlWheresList.Any((where) => where != null))
            sqlWheres = "WHERE " + string.Join(" AND ", sqlWheresList);

            return sqlWheres;
        }

        public string ColumnsSql(List<string> columns)
        {
            if (columns != null && columns.Any())
                return string.Join(",", columns);

            return "*";
        }

        public string WhereColumn(String columnName, dynamic value, ComparationSymbol symbolEnum = ComparationSymbol.EQUAL)
        {
            string comparationSymbol;

            switch (symbolEnum)
            {
                case ComparationSymbol.EQUAL:
                    comparationSymbol = "=";
                    break;
                case ComparationSymbol.GREATER_THAN:
                    comparationSymbol = ">";
                    break;
                default:
                    throw new Exception("Comparation symbol isn't in switch case statement");
            }

            var where = $"{columnName} {comparationSymbol} {AddQuotesIfNotNumericOrConvertIfBool(value)}";

            return where;
        }

        public string WhereColumnContains(string columnName, string value)
        {
            var where = $"{columnName} LIKE '%_{value}_%'";

            return where;
        }

        public string OrderBySql(string orderByColumn, int? limit = null, int? offset = null) {
            var orderBy = "";

            if (!string.IsNullOrWhiteSpace(orderByColumn))
                orderBy = $"ORDER BY {orderByColumn}";

            if (limit != null) orderBy += " LIMIT $limit";

            if (offset != null) orderBy += " OFFSET $offset";

            return orderBy;
        }

        public string GetLastInsertedRowId()
        {
            return "SELECT last_insert_rowid();";
        }

        public string Update<E>(E entity) where E : IEntity<E>
        {
            var entityDict = entity.DbTable.EntityMapToDatabaseIncludeId(entity);

            var idColumn = entity.DbTable.IdColumn;
            var setColumns = GetSqlSetColumnsOnUpdate(entityDict);

            var sql = $@"
                UPDATE {entity.DbTable.TableName}
                SET {setColumns}
                WHERE {idColumn} = {entityDict[idColumn]}
            ";

            return sql;
        }

        public string Update(string tableName,  Dictionary<string, dynamic> entity, string where)
        {
            var setColumns = GetSqlSetColumnsOnUpdate(entity);

            var sql = $"UPDATE {tableName} SET {setColumns} WHERE {where}";

            return sql;
        }

        private string GetSqlSetColumnsOnUpdate(Dictionary<string, dynamic> entity)
        {
            return string.Join(
                "\n, "
                , entity.Select(e => $"{e.Key} = {AddQuotesIfNotNumericOrConvertIfBool(e.Value)}")
            );
        }
    }
}