using DomainLayer.Data;
using Microsoft.EntityFrameworkCore;
using RepoLayer.IRepos;
using RepoLayer.Repos;
using Serilog;
using ServiceLayer.IServices;
using ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
/*
 * Log
 */
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.AddSerilog(Log.Logger);


/*
 * Database
 */
builder.Services.AddDbContext<KeepDailyContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                            ?? throw new NullReferenceException("Null DefaultConnection.");
    options.UseSqlServer(connectionString);
});

/*
 * Cors
 */
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                          policy.WithOrigins("https://notify-bot.line.me",
                                              "http://10.199.15.44")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<ILineNotifyService, LineNotifyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

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

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
