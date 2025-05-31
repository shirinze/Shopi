using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using Shopi.Application.Behaviours;
using Shopi.Application.ViewModels;
using Shopi.DomainModel.Attributes;
using Shopi.DomainModel.BaseModels;
using Shopi.DomainModel.Models.ProductAggrigate;
using Shopi.DomainService;
using Shopi.DomainService.Proxies;
using Shopi.DomainService.Repositories;
using Shopi.Infrastructure.Data;
using Shopi.Infrastructure.Data.DbContexts;
using Shopi.Infrastructure.Data.Repositories;
using Shopi.Infrastructure.Proxies;
using Shopi.WebApi.Features;
using Shopi.WebApi.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShopiDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITrackingCodeProxy, TrackingCodeProxy>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
builder.Services.AddMemoryCache();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddHttpClient<ITrackingCodeProxy, TrackingCodeProxy>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<Settings>>().Value.TrackingCode;
    client.BaseAddress = new Uri(settings.BaseURL);
})
.AddPolicyHandler(Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .OrResult(r => !r.IsSuccessStatusCode)
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt),
        onRetry: (response, delay, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} after {delay.TotalSeconds}s due to {response.Exception?.Message ?? response.Result?.StatusCode.ToString()}");
        }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();

var enumTypes = typeof(BaseEntity).Assembly
    .GetTypes()
    .Where(t => t.IsEnum && t.GetCustomAttributes(typeof(EnumEndPointAttribute), false).Length != 0)
.ToList();

foreach (var enumType in enumTypes)
{
    var attribute = (enumType.GetCustomAttribute(typeof(EnumEndPointAttribute)) as EnumEndPointAttribute)!;
    var route = attribute.Route;

    app.MapGet(route, () =>
    {
        var enumValues = Enum.GetValues(enumType)
                         .Cast<Enum>()
                         .Select(e => new EnumViewModel(e));

        return BaseResult.Success(enumValues);
    })
        .WithTags("Enums");
}

app.MapPost("/Products", async (ShopiDbContext db, CancellationToken cancellationToken) =>
{
    var shoes = new Shoes { Size = 37, Name = "Test1", Color = "Red", Price = 2500 };
    var gold = new Gold { Karat = 18, Name = "Test2", Price = 125000, };
    var phone = new Phone { Model = "s25", Name = "samsung", Price = 9000000 };

    await db.AddAsync(shoes, cancellationToken);
    await db.AddAsync(gold, cancellationToken);
    await db.AddAsync(phone, cancellationToken);
    await db.SaveChangesAsync(cancellationToken);
});

app.Run();
