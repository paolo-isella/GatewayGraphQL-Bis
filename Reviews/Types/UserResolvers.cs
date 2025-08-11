using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;
using Reviews.Types;
using Reviews;

namespace Reviews.Types;

[ExtendObjectType(typeof(User))]
public class UserResolvers
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews([Parent] User user, ReviewDbContext dbContext)
        => dbContext.Reviews.Where(r => r.UserId == user.Id);
}
