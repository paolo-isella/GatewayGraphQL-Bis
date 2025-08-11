using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Reviews.Types;

namespace Reviews;

public class Query
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews(ReviewDbContext dbContext) => dbContext.Reviews;

    public Task<Review?> GetReviewByIdAsync(int id, ReviewDbContext dbContext) =>
        dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Review>> GetReviewsByIdAsync(
        int[] ids,
        ReviewDbContext dbContext
    ) => await dbContext.Reviews.Where(r => ids.Contains(r.Id)).ToListAsync();

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviewsByUserId(int[] userIds, ReviewDbContext dbContext) =>
        dbContext.Reviews.Where(r => userIds.Contains(r.UserId));

    public Task<IEnumerable<User>> GetUsersByIdAsync(int[] ids) =>
        Task.FromResult(ids.Select(id => new User { Id = id }));
}
