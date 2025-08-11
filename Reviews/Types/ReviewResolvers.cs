namespace Reviews.Types;

public sealed class ReviewType : ObjectType<Review>
{
    // public User GetUser([Parent] Review review) => new User { Id = review.UserId };

    protected override void Configure(IObjectTypeDescriptor<Review> d)
    {
        d.Field("user")
            .Type<NonNullType<ObjectType<User>>>()
            .Resolve(ctx =>
            {
                var review = ctx.Parent<Review>();
                return new User { Id = review.UserId }; // stub entity
            });
        
        d.Field(x => x.LifeCycles).UseFiltering();
    }
    
    // [UseFiltering]
    // [UseSorting]
    // public Task<IReadOnlyList<LifeCycle>> GetLifeCycles(
    //     [Parent] Review review,
    //     QueryContext<LifeCycle> query,
    //     LifeCyclesByReviewIdDataLoader dl,
    //     CancellationToken ct)
    //     => dl.With(query).LoadAsync(review.Id, ct);
}
