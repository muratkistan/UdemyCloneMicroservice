using System.Security.Principal;
using UdemyCloneMicroservice.Basket.Api;
using UdemyCloneMicroservice.Basket.Api.Features;
using UdemyCloneMicroservice.Basket.Api.Features.Baskets;
using UdemyCloneMicroservice.Shared.Extensions;
using UdemyCloneMicroservice.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddScoped<BasketService>();

builder.Services.AddScoped<IIdentityService, IdentityServiceFake>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddVersioningExt();

var app = builder.Build();

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();