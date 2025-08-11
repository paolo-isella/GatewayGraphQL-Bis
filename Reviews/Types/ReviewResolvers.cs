using HotChocolate;
using HotChocolate.Types;

namespace Reviews.Types;

[ExtendObjectType(typeof(Review))]
public class ReviewResolvers
{
    public User GetUser([Parent] Review review) => new User { Id = review.UserId };
}
