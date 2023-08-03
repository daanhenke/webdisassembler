using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;

namespace WebDisassembler.DataStorage.Utility;

public static class QueryableExtensions
{
    public static async ValueTask<PagedResponse<TModel>> ToPaged<TModel>(this IQueryable<TModel> queryable, PagedRequest request)
    {
        var total = await queryable.CountAsync();
        var items = await queryable
            .Skip(request.Index * request.Size)
            .Take(request.Size)
            .ToArrayAsync();

        return new(total, items);
    }
}