using System.Collections.Generic;
using DesafioBack.Services.Videos.Filters;

namespace DesafioBack.Services.Shared
{
    public interface IFilterQueryBuilder
    {
        List<string> Columns { get; }
        VideoFilterQueryBuilder SetColumns(List<string> columns);

        int Limit { get; }
        VideoFilterQueryBuilder SetLimit(int limit);

        int Offset { get; }

        VideoFilterQueryBuilder SetOffset(int offset);

        string OrderBy { get; }

        VideoFilterQueryBuilder SetOrderBy(string orderBy);

        string BuildRawQuery();
    }
}