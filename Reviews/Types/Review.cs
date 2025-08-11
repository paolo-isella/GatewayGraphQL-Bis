using GreenDonut.Data;
using Microsoft.EntityFrameworkCore;

namespace Reviews.Types;

public class Review
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public int Stars { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public List<LifeCycle> LifeCycles { get; set; } = [];

    public int UserId { get; set; }
}
