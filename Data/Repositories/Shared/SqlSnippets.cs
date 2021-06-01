using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioBack.Data.Repositories.Shared
{
    public class SqlSnippets
    {
        private static SqlSnippets _instance = new SqlSnippets();
        public static SqlSnippets Instance { get => _instance; }

        public string InsertMultiple(string tableName, List<Dictionary<string, dynamic>> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException($"[entities] is null or empty");

            var firstEntity = entities.First();

            var values = string.Join("," , entities.Select(entity => $"({string.Join(",", entity.Values.Select(AddQuotesIfNotNumeric))})"));

            var sql = $@"
                INSERT INTO {tableName} ({string.Join(",", firstEntity.Keys)})
                VALUES {values}
            ";
            
            return sql;
        }

        private string AddQuotesIfNotNumeric(dynamic value)
        {
            double _;
            var isNumeric = double.TryParse(value.ToString(), out _);

            if (isNumeric) return value.toString();

            return $"'{value}'";
        }

        public string WheresSql(List<string> sqlWheresList) {
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

        public string WhereColumnEquals(String columnName, dynamic value)
        {
            var where = $"{columnName} = {AddQuotesIfNotNumeric(value)}";

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
    }
}