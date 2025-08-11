using Microsoft.EntityFrameworkCore;
using Reviews.Types;

namespace Reviews;

[QueryType]
public static class Query
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Review> GetReviews(ReviewDbContext dbContext) => dbContext.Reviews.AsSplitQuery();

    public static Task<Review?> GetReviewByIdAsync(int id, ReviewDbContext dbContext) =>
        dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);

    public static async Task<IEnumerable<Review>> GetReviewsByIdAsync(
        int[] ids,
        ReviewDbContext dbContext
    ) => await dbContext.Reviews.Where(r => ids.Contains(r.Id)).ToListAsync();

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Review> GetReviewsByUserId(int[] userIds, ReviewDbContext dbContext) =>
        dbContext.Reviews.Where(r => userIds.Contains(r.UserId));

    public static Task<IEnumerable<User>> GetUsersByIdAsync(int[] ids) =>
        Task.FromResult(ids.Select(id => new User { Id = id }));
}
