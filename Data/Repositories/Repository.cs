using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioBack.Data.Repositories.shared;
using DesafioBack.Data.Shared;
using DesafioBack.Models;
using DesafioBack.Models.Shared;

namespace DesafioBack.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly IMyDatabase _database;
        private readonly ISqlSnippets _sqlSnippets;

        public Repository(IMyDatabase database, ISqlSnippets sqlSnippets)
        {
            _database = database;
            _sqlSnippets = sqlSnippets;
        }

        public async Task<long> Insert<E>(E entity) where E : IEntity<E>
        {
            var sql = _sqlSnippets.Insert(entity);

            sql += $@"
                {_sqlSnippets.GetLastInsertedRowId()}
            ";

            var entityId = (long) await _database.ExecuteScalarAsync(sql);

            return entityId;
        }

        public async Task Insert<E>(List<E> entities) where E : IEntity<E>
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException($"[entities] is null or empty");

            var firstEntity = entities.First();

            var sql = _sqlSnippets.Insert(entities);

            await _database.ExecuteNonQueryAsync(sql);
        }

        public async Task<List<E>> FindByQuery<E>(string sql) where E : IEntity<E>, new()
        {
            var dictList = await _database.ExecuteQueryAsync(sql);

            var entities = new E().DbTable.EntityMapFromDatabase(dictList);

                return entities;
        }

        public async Task Update<E>(E entity) where E : IEntity<E>
        {
            var sql = _sqlSnippets.Update(entity);

            await _database.ExecuteNonQueryAsync(sql);
        }

        public async Task UpdateWherePkEquals<E>(long id, IDbTable<E> instance, Dictionary<string, dynamic> entity) where E : IEntity<E>
        {
            var where = $"{instance.IdColumn} = {id}";

            var sql = _sqlSnippets.Update(instance.TableName, entity, where);

            await _database.ExecuteNonQueryAsync(sql);
        }
    }
}