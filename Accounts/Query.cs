using Accounts.Models;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data;

namespace Accounts;

public class Query
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(AccountDbContext dbContext)
        => dbContext.Users;

    public Task<User?> GetUserByIdAsync(int id, AccountDbContext dbContext)
        => dbContext.Users.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<User>> GetUsersByIdAsync(int[] ids, AccountDbContext dbContext)
        => await dbContext.Users.Where(t => ids.Contains(t.Id)).ToListAsync();

    public Task<User?> GetUserByUsernameAsync(string username, AccountDbContext dbContext)
        => dbContext.Users.FirstOrDefaultAsync(t => t.Username == username);
}
