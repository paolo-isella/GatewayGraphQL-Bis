using Accounts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        //https://studio.apollographql.com/sandbox/explorer
        options.AddDefaultPolicy(b => 
            b.WithOrigins("https://nitro.chillicream.com").AllowAnyHeader().AllowAnyMethod());
    });

    builder.Services.AddDbContextFactory<AccountDbContext>(opt => opt
        .UseSqlite("Data Source=app.db")
        .EnableSensitiveDataLogging());
}

var app = builder.Build();
{
    app.UseCors();
    app.MapGraphQL();
    app.RunWithGraphQLCommands(args);
}