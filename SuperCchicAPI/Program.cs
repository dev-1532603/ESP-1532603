using Microsoft.EntityFrameworkCore;
using SuperCchicAPI;
using SuperCchicAPI.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var iniPath = Path.Combine(AppContext.BaseDirectory, "config.ini");
var db = IniReader.LoadDatabaseSettings(iniPath);

builder.Services.AddDbContext<SuperCchicContext>(options =>
{
    var connectionString =
    $"Server={db.Server};Port={db.Port};Database={db.Database};User={db.User};Password={db.Password};";
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

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

app.Run();
