using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MyFinance.Persistence;
using MyFinance.Application.Common.Mappings;
using MyFinance.Application.Interfaces;
using MyFinance.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
         x => x.MigrationsAssembly("MyFinance.Persistence"));
});
builder.Services.AddScoped<IDataDbContext, DataDbContext>();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IDataDbContext).Assembly));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IDataDbContext).Assembly));
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
