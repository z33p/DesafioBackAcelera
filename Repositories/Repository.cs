using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using DesafioBack.Database;
using DesafioBack.Models.Shared;
using DesafioBack.Repositories.Shared;

namespace DesafioBack.Repositories
{
    public class Repository
    {
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

            await MyDatabase.Instance.ExecuteQuery();
        }
    }
}