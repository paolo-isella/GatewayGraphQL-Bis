using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Reviews;
using Reviews.Types;

namespace Reviews.Types;

[ExtendObjectType(typeof(User))]
public class UserResolvers
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews([Parent] User user, ReviewDbContext dbContext) =>
        dbContext.Reviews.Where(r => r.UserId == user.Id);
}
