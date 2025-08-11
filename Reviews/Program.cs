using Microsoft.EntityFrameworkCore;
using Reviews;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        //https://studio.apollographql.com/sandbox/explorer
        options.AddDefaultPolicy(b =>
            b.WithOrigins("https://nitro.chillicream.com").AllowAnyHeader().AllowAnyMethod()
        );
    });

    builder.Services.AddDbContextFactory<ReviewDbContext>(opt =>
        opt.UseSqlite("Data Source=app.db").EnableSensitiveDataLogging()
    );

    builder.Services
        .AddGraphQLServer()
        .AddQueryType<Query>()
        .AddTypeExtension<Reviews.Types.UserResolvers>()
        .AddTypeExtension<Reviews.Types.ReviewResolvers>()
        .AddFiltering()
        .AddSorting()
        .AddProjections();
}

var app = builder.Build();
{
    app.UseCors();
    app.MapGraphQL();
    app.RunWithGraphQLCommands(args);
}
