using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace Reviews.Types;

[ExtendObjectType(typeof(User))]
public class UserResolvers
{
    [UseFiltering]
    [UseSorting]
    public Task<Review[]> GetReviews(
        [Parent] User user,
        QueryContext<Review> query,
        ReviewsByUserDataLoader  loader,
        CancellationToken cancellationToken
    ) =>
        loader.With(query).LoadAsync(user.Id, cancellationToken);
}

public static class DataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, Review[]>> GetReviewsByUserAsync(
        IReadOnlyList<int> userIds,
        QueryContext<Review> query,
        ReviewDbContext db,
        CancellationToken ct)
    {
        var items = await db.Reviews
            .Where(r => userIds.Contains(r.UserId))
            .With(query)
            .ToListAsync(ct);

        return items
            .GroupBy(r => r.UserId)
            .ToDictionary(g => g.Key, g => g.ToArray());
    }
}


