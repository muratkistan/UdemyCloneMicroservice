using Microsoft.EntityFrameworkCore;
using UdemyCloneMicroservice.Payment.Api;
using UdemyCloneMicroservice.Payment.Api.Feature.Payments;
using UdemyCloneMicroservice.Payment.Api.Repositories;
using UdemyCloneMicroservice.Shared.Extensions;
using UdemyCloneMicroservice.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddScoped<IIdentityService, IdentityServiceFake>();
builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("payment-db"); });

var app = builder.Build();

app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();