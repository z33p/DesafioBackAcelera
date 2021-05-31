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
    }
}