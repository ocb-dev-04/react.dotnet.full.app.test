using Api;
using Persistence;
using Api.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServices();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.CheckMigrations();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();
app.UseResponseCompression();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program;