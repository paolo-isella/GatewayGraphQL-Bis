using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace Reviews.Types;

[ObjectType<Review>]
public static partial class ReviewType
{
    public static User GetUser([Parent] Review review) => new User { Id = review.UserId };

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static Task<LifeCycle[]?> GetLifeCycles(
        [Parent] Review review,
        QueryContext<LifeCycle> query,
        LifeCyclesByReviewIdDataLoader loader,
        CancellationToken ct)
        => loader.With(query).LoadAsync(review.Id, ct);
}

public static class ReviewDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, LifeCycle[]>> GetLifeCyclesByReviewIdAsync(
        IReadOnlyList<int> reviewIds,
        QueryContext<LifeCycle> query,
        ReviewDbContext db,
        CancellationToken ct)
    {
        var items = await db.LifeCycles
            .Where(lc => reviewIds.Contains(lc.ReviewId))
            .With(query)
            .ToListAsync(ct);

        var result = items
            .GroupBy(lc => lc.ReviewId)
            .ToDictionary(g => g.Key, g => g.ToArray());

        foreach (var id in reviewIds)
        {
            if (!result.ContainsKey(id))
            {
                result[id] = [];
            }
        }

        return result;
    }
}