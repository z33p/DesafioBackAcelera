using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using DesafioBack.Data;
using DesafioBack.Models.Shared;

namespace DesafioBack.Models.Tables
{
    public class VideoTable : IDbTable<Video>
    {
        private static VideoTable _instance = new VideoTable();
        public static VideoTable Instance { get => _instance; } 

        public string TableName { get; set; } = "videos";

        public const string IdColumn = "video_id";
        public const string TitleColumn = "title";
        public const string AuthorColumn = "author";
        public const string DurationColumn = "duration";

        public const string PublishedAtColumn = "published_at";

        public async Task CreateTable(SQLiteConnection connection)
        {
            var command = connection.CreateCommand();

            command.CommandText = $@"
                DROP TABLE IF EXISTS {TableName};

                CREATE TABLE IF NOT EXISTS {TableName} (
                    {IdColumn} TEXT PRIMARY KEY
                    , {TitleColumn} TEXT
                    , {AuthorColumn} TEXT
                    , {PublishedAtColumn} TEXT
                );
            ";

            await command.ExecuteNonQueryAsync();
        }

        public Dictionary<string, dynamic> EntityMapToDatabase(Video video) => new Dictionary<string, dynamic>
        {
            [IdColumn] = video.VideoId
            , [TitleColumn] = video.Title
            , [AuthorColumn] = video.Author
            , [PublishedAtColumn] = video.PublishedAt
        };

        public List<Dictionary<string, dynamic>> EntityMapToDatabase(List<Video> videos)
        {
            return videos.Select(v => EntityMapToDatabase(v)).ToList();
        }

        public Video EntityMapFromDatabase(Dictionary<string, dynamic> dict)
        {
            var video = new Video();

            if (dict.ContainsKey(IdColumn))
                video.VideoId = dict[IdColumn];

            if (dict.ContainsKey(TitleColumn))
                video.Title = dict[TitleColumn];

            if (dict.ContainsKey(AuthorColumn))
                video.Author = dict[AuthorColumn];

            if (dict.ContainsKey(DurationColumn))
                video.Duration = dict[DurationColumn];

            if (dict.ContainsKey(PublishedAtColumn))
                video.PublishedAt = DateTime.Parse((string) dict[PublishedAtColumn]);

            return video;
        }

        public List<Video> EntityMapFromDatabase(List<Dictionary<string, dynamic>> dictList)
        {
            return dictList.Select(d => EntityMapFromDatabase(d)).ToList();
        }
    }
}