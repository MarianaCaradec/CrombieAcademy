using BibliotecaAPIWeb.Data;
using BibliotecaAPIWeb.InterfacesServices;
using BibliotecaAPIWeb.Utilities;
using BibliotecaAPIWeb.Middleware;
using BibliotecaAPIWeb.Services;

var builder = WebApplication.CreateBuilder(args);

var logFilePath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "Logs",
    "api-errors.log");

Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new FileLogger(logFilePath));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorLoggingMiddleware>();

app.MapControllers();

app.Run();
