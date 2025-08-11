using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Reviews;
using Reviews.Types;
using System.Threading;

namespace Reviews.DataLoaders;

public class ReviewsByUserIdDataLoader : BatchDataLoader<int, IReadOnlyList<Review>>
{
    private readonly IDbContextFactory<ReviewDbContext> _dbContextFactory;

    public ReviewsByUserIdDataLoader(
        IBatchScheduler batchScheduler,
        IDbContextFactory<ReviewDbContext> dbContextFactory,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<int, IReadOnlyList<Review>>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var reviews = await dbContext.Reviews
            .Where(r => keys.Contains(r.UserId))
            .ToListAsync(cancellationToken);

        var grouped = reviews
            .GroupBy(r => r.UserId)
            .ToDictionary(g => g.Key, g => (IReadOnlyList<Review>)g.ToList());

        foreach (var key in keys)
        {
            if (!grouped.ContainsKey(key))
            {
                grouped[key] = Array.Empty<Review>();
            }
        }

        return grouped;
    }
}
