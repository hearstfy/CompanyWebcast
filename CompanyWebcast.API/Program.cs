using CompanyWebcast.Application;
using CompanyWebcast.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using System.Reflection;
using ApplicationException = CompanyWebcast.Application.Common.Exceptions.ApplicationException;

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext httpContext) =>
{
    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    
    if (exception?.GetType().BaseType == typeof(ApplicationException))
    {
        return Results.Problem(title: exception.Message, statusCode: ((ApplicationException)exception).StatusCode);
    }

    return Results.Problem(title: exception?.Message);
});
app.MapControllers();

app.Run();
