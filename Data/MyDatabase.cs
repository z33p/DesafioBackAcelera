using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using DesafioBack.Data.Shared;
using DesafioBack.Models.Tables;

namespace DesafioBack.Data
{
    public class MyDatabase : IMyDatabase
    {
        public const string DatabasePath = "Data/database.db";
        public const string SqliteConnectionString = "Data Source=" + DatabasePath;
        
        public async Task InitDatabase(Func<Task> OnDbInitialized)
        {
            if (File.Exists(DatabasePath)) return;

            using (var connection = new SQLiteConnection(SqliteConnectionString))
            {
                connection.Open();

                await VideoTable.Instance.CreateTable(connection);
            }

            await OnDbInitialized();
        }

        public async Task ExecuteNonQueryAsync(string sql)
        {
            using (var connection = new SQLiteConnection(MyDatabase.SqliteConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = sql;

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Dictionary<string, dynamic>>> ExecuteQueryAsync(string sql)
        {
            using (var connection = new SQLiteConnection(MyDatabase.SqliteConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = sql;

                var dictList = await GetDictListWithQueryRowsCapacity(sql);

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

                return dictList;
            }
        }

        private async Task<List<Dictionary<string, dynamic>>> GetDictListWithQueryRowsCapacity(string sql)
        {
            var totalRows = await GetTotalRowsFromQuery(sql);

            var dictList = new List<Dictionary<string, dynamic>>(totalRows);

            return dictList;
        }

        private async Task<int> GetTotalRowsFromQuery(string sql)
        {
            var fromSql = "FROM " + sql.Split(new string[] { "FROM" }, StringSplitOptions.RemoveEmptyEntries)[1];
            
            sql = $"SELECT COUNT(*) {fromSql}";

            var totalRows = Convert.ToInt32(await ExecuteScalarAsync(sql));
            return totalRows;
        }

        public async Task<object> ExecuteScalarAsync(string sql)
        {
            using (var connection = new SQLiteConnection(MyDatabase.SqliteConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = sql;

                return await command.ExecuteScalarAsync();
            }
        }
    }
}