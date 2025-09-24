using Microsoft.EntityFrameworkCore;
using UdemyCloneMicroservice.Bus;
using UdemyCloneMicroservice.Order.Api;
using UdemyCloneMicroservice.Order.Api.Endpoints.Orders;
using UdemyCloneMicroservice.Order.Application;
using UdemyCloneMicroservice.Order.Application.Contracts.Repositories;
using UdemyCloneMicroservice.Order.Application.UnitOfWork;
using UdemyCloneMicroservice.Order.Persistence;
using UdemyCloneMicroservice.Order.Persistence.Repositories;
using UdemyCloneMicroservice.Order.Persistence.UnitOfWork;
using UdemyCloneMicroservice.Shared.Extensions;
using UdemyCloneMicroservice.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
//builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(OrderApiAssembly)));
builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly("UdemyCloneMicroservice.Order.Persistence");
        });
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();
app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseAuthorization();

app.Run();