using MongoDB.Driver;
using UdemyCloneMicroservice.Catalog.Api;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories;
using UdemyCloneMicroservice.Catalog.Api.Features.Courses;
using UdemyCloneMicroservice.Catalog.Api.Options;
using UdemyCloneMicroservice.Catalog.Api.Repositories;
using UdemyCloneMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersioningExt();

var app = builder.Build();

app.AddSeedDataExt().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed data has been saved successfully");
});

app.AddCategoryGroupEndpointExt(app.AddVersionSetExt());
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Catalog API V1");
    });
}

app.Run();