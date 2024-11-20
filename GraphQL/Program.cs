using GraphQL;
using GraphQL.AspNet.Configuration;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQL();
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Local")));
var app = builder.Build();
app.UseHttpsRedirection();
app.UseGraphQL();
app.Run();
