using System;
using System.Collections.Generic;
using DesafioBack.Data.Shared;
using DesafioBack.Models.Tables;
using DesafioBack.Services.Shared;

namespace DesafioBack.Services.Videos.Filters
{
    public class VideoFilterQueryBuilder : IFilterQueryBuilder
    {

        private readonly ISqlSnippets _sqlSnippets;

        public VideoFilterQueryBuilder(ISqlSnippets sqlSnippets)
        {
            _sqlSnippets = sqlSnippets;
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

        public long? Duration { get; private set; }
        public VideoFilterQueryBuilder SetDuration(long duration)
        {
            this.Duration = duration;
            return this;
        }

        public VideoFilterQueryBuilder SetDuration(string duration)
        {
            this.Duration = VideosService.ConvertDurationToTicks(duration);
            return this;
        }

        public DateTime? PublishedAt { get; private set; }
        public VideoFilterQueryBuilder SetPublishedAt(DateTime publishedAt)
        {
            this.PublishedAt = publishedAt;
            return this;
        }
        
        public DateTime? PublishedAfter { get; private set; }
        public VideoFilterQueryBuilder SetPublishedAfter(DateTime publishedAfter)
        {
            this.PublishedAfter = publishedAfter;
            return this;
        }

        public bool? Deleted { get; private set; } = false;
        public VideoFilterQueryBuilder SetDeleted(bool? deleted)
        {
            this.Deleted = deleted;
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

            if (!string.IsNullOrWhiteSpace(this.Title))
                wheresList.Add(_sqlSnippets.WhereColumnContains(VideoTable.TitleColumn, this.Title));
            
            if (!string.IsNullOrWhiteSpace(this.Author))
                wheresList.Add(_sqlSnippets.WhereColumnContains(VideoTable.AuthorColumn, this.Author));
            
            if (this.Duration != null)
                wheresList.Add(_sqlSnippets.WhereColumn(VideoTable.DurationColumn, this.Duration));
            
            if (this.PublishedAt != null)
                wheresList.Add(_sqlSnippets.WhereColumn(VideoTable.PublishedAtColumn, this.PublishedAt));
            else if (this.PublishedAfter != null)
                wheresList.Add(
                    _sqlSnippets.WhereColumn(
                        VideoTable.PublishedAtColumn
                        , this.PublishedAfter
                        , Data.Repositories.shared.ComparationSymbol.GREATER_THAN
                    )
                );

            if (this.Deleted.HasValue)
                wheresList.Add(_sqlSnippets.WhereColumn(VideoTable.DeletedColumn, this.Deleted));

            var columnsSql = _sqlSnippets.ColumnsSql(this.Columns);
 
            var wheresSql = _sqlSnippets.WheresSql(wheresList);
 
            var orderBySql = _sqlSnippets.OrderBySql(this.OrderBy);

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