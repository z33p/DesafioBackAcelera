using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Data.Repositories.Shared;
using DesafioBack.Models.Shared;
using Microsoft.Extensions.Logging;

namespace DesafioBack.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;

        public Repository(ILogger<Repository> logger)
        {
            _logger = logger;
        }

        public async Task Insert<E>(List<E> entities) where E : IEntity<E>
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException($"[entities] is null or empty");

            var firstEntity = entities.First();

            var sql = SqlSnippets.Instance.InsertMultiple(
                firstEntity.DbTable.TableName
                , firstEntity.DbTable.EntityMapToDatabase(entities)
            );

            using (var connection = new SQLiteConnection(MyDatabase.SqliteConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = sql;

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<E>> FindByQuery<E>(string rawQuery) where E : IEntity<E>, new()
        {
            using (var connection = new SQLiteConnection(MyDatabase.SqliteConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                var dictList = GetDictListWithQueryCountCapacity(cmd, rawQuery);

                cmd.CommandText = rawQuery;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // one excution per row
                        var dict = new Dictionary<string, dynamic>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            // x executions per columns
                            dict.Add(reader.GetName(i), reader.GetValue(i)); 
                        }

                        dictList.Add(dict);
                    }
                }

                var entities = new E().DbTable.EntityMapFromDatabase(dictList);

                return entities;
            }
        }

        private List<Dictionary<string, dynamic>> GetDictListWithQueryCountCapacity(SQLiteCommand cmd, string rawQuery)
        {
            var fromSql = "FROM " + rawQuery.Split(new string[] { "FROM" }, StringSplitOptions.RemoveEmptyEntries)[1];
            cmd.CommandText = $"SELECT COUNT(*) {fromSql}";

            var totalRows = Convert.ToInt32(cmd.ExecuteScalar());

            var dictList = new List<Dictionary<string, dynamic>>(totalRows);

            return dictList;
        }
    }
}