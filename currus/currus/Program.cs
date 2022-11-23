using System.Text.Json.Serialization;
using currus.Data;
using currus.Logging.Logic;
using currus.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p =>
    p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

builder.Services.AddScoped<IUserDbRepository, UserDbRepository>();
builder.Services.AddScoped<ITripDbRepository, TripDbRepository>();
//builder.Services.AddScoped<IUserStatusEventHandler, UserStatusEventHandler>();

builder.Services.AddControllers().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
    );

var app = builder.Build();

// Create a LOG folder with file
Logger.createLogFile();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseCors("corsapp");
app.UseAuthorization();
app.UseRouting();

app.MapControllers();

app.Run();
public partial class Program { }