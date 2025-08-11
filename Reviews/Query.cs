using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;
using Reviews.Types;

namespace Reviews;

public class Query
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews(ReviewDbContext dbContext)
        => dbContext.Reviews;

    public Task<Review?> GetReviewByIdAsync(int id, ReviewDbContext dbContext)
        => dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Review>> GetReviewsByIdAsync(int[] ids, ReviewDbContext dbContext)
        => await dbContext.Reviews.Where(r => ids.Contains(r.Id)).ToListAsync();

    public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int[] userIds, ReviewDbContext dbContext)
        => await dbContext.Reviews.Where(r => userIds.Contains(r.UserId)).ToListAsync();
}
