using CompanyWebcast.Application;
using CompanyWebcast.Application.Common.Exceptions;
using CompanyWebcast.Infrastructure;
using CompanyWebcast.Infrastructure.Persistance;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext httpContext) =>
{
    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    switch (exception)
    {
        case ForecastAlreadyExistsException:
            return Results.Problem(title: exception.Message, statusCode: (int)HttpStatusCode.Conflict);
        case ForecastDoesNotExistsException e:
            return Results.Problem(title: exception.Message, statusCode: (int)HttpStatusCode.NotFound);
        default:
            return Results.Problem(title: exception.Message);
    }
});
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDBContext>();
    context.Database.Migrate();
}


app.Run();
