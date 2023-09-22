using Middleware.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

var logFile = Path.Combine(Environment.CurrentDirectory, "log.txt");

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(logFile)
    .CreateLogger();

app.UseAuthorization();

app.UseMiddleware<VpnDetectMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
