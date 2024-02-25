using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Options;
using StoreApp.DatabaseProvider;
using StoreApp.Middlewares;
using StoreApp.Services.Interfaces;
using StoreApp.Services.Services;
using Stripe;
using ProductService = StoreApp.Services.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddDbContext<DataContext>(context => context.UseNpgsql(builder.Configuration.GetConnectionString("MainDb")), ServiceLifetime.Scoped);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("ApiOptions"));

var app = builder.Build();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
