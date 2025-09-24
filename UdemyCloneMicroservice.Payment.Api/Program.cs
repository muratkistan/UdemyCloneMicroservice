using Microsoft.EntityFrameworkCore;
using UdemyCloneMicroservice.Bus;
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
builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("payment-db"); });

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();