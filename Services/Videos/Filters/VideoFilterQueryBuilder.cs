using System;
using System.Collections.Generic;
using DesafioBack.Data.Repositories.Shared;
using DesafioBack.Models.Tables;
using DesafioBack.Services.Shared;

namespace DesafioBack.Services.Videos.Filters
{
    public class VideoFilterQueryBuilder : IFilterQueryBuilder
    {
        public string Id { get; private set; }
        public VideoFilterQueryBuilder SetId(string id)
        {
            this.Id = id;
            return this;
        }

        public string Title { get; private set; }
        public VideoFilterQueryBuilder SetTitle(string title)
        {
            this.Title = title;
            return this;
        }

        public string Author { get; private set; }
        public VideoFilterQueryBuilder SetAuthor(string author)
        {
            this.Author = author;
            return this;
        }

        public int? Duration { get; private set; }
        public VideoFilterQueryBuilder SetDuration(int duration)
        {
            this.Duration = duration;
            return this;
        }

        public VideoFilterQueryBuilder SetDuration(string duration)
        {
            this.Duration = VideosService.ConvertDurationToTotalSeconds(duration);
            return this;
        }

        public DateTime? PublishedAt { get; private set; }
        public VideoFilterQueryBuilder SetPublishedAt(DateTime publishedAt)
        {
            this.PublishedAt = publishedAt;
            return this;
        }
        
        public DateTime PublishedAfter { get; private set; }
        public VideoFilterQueryBuilder SetPublishedAfter(DateTime publishedAfter)
        {
            this.PublishedAfter = publishedAfter;
            return this;
        }

        public List<string> Columns { get; private set; }
        public VideoFilterQueryBuilder SetColumns(List<string> columns)
        {
            this.Columns = columns;
            return this;
        }

        public string OrderBy { get; private set; }
        public VideoFilterQueryBuilder SetOrderBy(string orderBy)
        {
            this.OrderBy = orderBy;
            return this;
        }

        public int Limit { get; private set; }
        public VideoFilterQueryBuilder SetLimit(int limit)
        {
            this.Limit = limit;
            return this;
        }

        public int Offset { get; private set; }
        public VideoFilterQueryBuilder SetOffset(int offset)
        {
            this.Offset = offset;
            return this;
        }

        public string BuildRawQuery()
        {
            var wheresList = new List<string>();

            if (!string.IsNullOrWhiteSpace(this.Id))
                wheresList.Add(SqlSnippets.Instance.WhereColumnEquals(VideoTable.IdColumn, this.Id));
            
            if (!string.IsNullOrWhiteSpace(this.Title))
                wheresList.Add(SqlSnippets.Instance.WhereColumnEquals(VideoTable.TitleColumn, this.Title));
            
            if (!string.IsNullOrWhiteSpace(this.Author))
                wheresList.Add(SqlSnippets.Instance.WhereColumnEquals(VideoTable.AuthorColumn, this.Author));
            
            if (this.Duration != null)
                wheresList.Add(SqlSnippets.Instance.WhereColumnEquals(VideoTable.DurationColumn, this.Duration));
            
            if (this.PublishedAt != null)
                wheresList.Add(SqlSnippets.Instance.WhereColumnEquals(VideoTable.PublishedAtColumn, this.PublishedAt));

            var columnsSql = SqlSnippets.Instance.ColumnsSql(this.Columns);
 
            var wheresSql = SqlSnippets.Instance.WheresSql(wheresList);
 
            var orderBySql = SqlSnippets.Instance.OrderBySql(this.OrderBy);

            var baseQuery = $@"
                SELECT
                    {columnsSql}
                FROM
                    {VideoTable.Instance.TableName}
                {wheresSql}
                {orderBySql}
            ";

            return baseQuery;
        }
    }
}