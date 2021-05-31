using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using DesafioBack.Models.Tables;

namespace DesafioBack.Data
{
    public class MyDatabase
    {
        private static MyDatabase _instance = new MyDatabase();
        public static MyDatabase Instance { get => _instance; } 

        public const string SqliteConnectionString = "Data Source=Data/database.db";
        
        public async Task InitDatabase()
        {
            using (var connection = new SQLiteConnection(SqliteConnectionString))
            {
                connection.Open();

                await VideoTable.Instance.CreateTable(connection);
            }
        }

        public async Task ExecuteQuery()
        {
            using (var connection = new SQLiteConnection(SqliteConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"
                    SELECT COUNT(*) FROM videos
                ";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetValue(0);

                        Console.WriteLine($"TOTAL = {name}");
                    }
                }
            }
        }
    }
}