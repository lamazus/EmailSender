using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using EmailSender.Infrastructure;
using EmailSender.Infrastructure.Services;
using EmailSender.Infrastructure.Settings;
using FluentValidation;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.Configure<SmtpSettings>(builder.Configuration.GetSection(nameof(SmtpSettings)));

services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
}
);

services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddFluentValidationAutoValidation();
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.GetCultureInfo("ru-RU");
services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
services.AddAutoMapper(Assembly.GetExecutingAssembly());

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EmailSender API",
        Version = "v1"
    });
    var xmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    options.IncludeXmlComments(xmlFile);   
});


services.AddTransient<SmtpService>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    
    dbContext.Database.EnsureDeleted(); 
    dbContext.Database.EnsureCreated();

}

app.UseHttpLogging();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}"
);

app.Run();
