using CompanyEmployees.Extensions;
using CompanyEmployees.Presentation;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using NLog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config"));

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

WebApplication app = builder.Build();

ILoggerManager logger = app.Services.GetRequiredService<ILoggerManager>();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();