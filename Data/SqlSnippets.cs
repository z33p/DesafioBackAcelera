using System;
using System.Collections.Generic;
using System.Linq;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Data.Shared;

namespace DesafioBack.Data
{
    public class SqlSnippets : ISqlSnippets
    {
        public string Insert(string tableName, Dictionary<string, dynamic> entity)
        {
            var values = GetValueColumnsFromEntity(entity);

            var sql = $@"
                INSERT INTO {tableName} ({string.Join(",", entity.Keys)})
                VALUES {values}
            ";

            return sql;
        }

        public string Insert(string tableName, List<Dictionary<string, dynamic>> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException($"[entities] is null or empty");

            var firstEntity = entities.First();

            var values = string.Join("," , entities.Select(entity => GetValueColumnsFromEntity(entity)));

            var sql = $@"
                INSERT INTO {tableName} ({string.Join(",", firstEntity.Keys)})
                VALUES {values}
            ";
            
            return sql;
        }

        private string GetValueColumnsFromEntity(Dictionary<string, dynamic> entity)
        {
            return $"({string.Join(",", entity.Values.Select(AddQuotesIfNotNumeric))})";
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

            var where = $"{columnName} {comparationSymbol} {AddQuotesIfNotNumeric(value)}";

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
    }
}