using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Reviews;
using Reviews.Types;

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

    builder
        .Services.AddGraphQLServer()
        .AddQueryType<Query>()
        .AddTypeExtension<Reviews.Types.UserResolvers>()
        .AddType<ReviewType>()
        .AddFiltering()
        .AddSorting()
        .AddProjections()
        .AddQueryContext();
}

var app = builder.Build();
{
    app.UseCors();
    app.MapGraphQL();
    app.RunWithGraphQLCommands(args);
}
