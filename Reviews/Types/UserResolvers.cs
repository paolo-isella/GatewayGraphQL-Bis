using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Reviews;
using Reviews.Types;
using System.Threading;

namespace Reviews.Types;

[ExtendObjectType(typeof(User))]
public class UserResolvers
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<Review>> GetReviews(
        [Parent] User user,
        DataLoaders.ReviewsByUserIdDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        var reviews = await dataLoader.LoadAsync(user.Id, cancellationToken);
        return reviews.AsQueryable();
    }
}
