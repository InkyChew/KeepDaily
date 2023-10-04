using DomainLayer.Data;
using DomainLayer.Dto;
using keepdaily_be.Middlewares;
using Microsoft.EntityFrameworkCore;
using Mymvc.Services;
using RepoLayer.IRepos;
using RepoLayer.Repos;
using Serilog;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using ServiceLayer.Utils;
using System.Text.Json.Serialization;

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
                            ?? throw new NullReferenceException("Null Database ConnectionString.");
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
                          policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                          //policy
                                .WithOrigins("http://localhost:4200",
                                                "https://notify-bot.line.me",
                                              "http://10.199.15.44")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

builder.Services.AddAutoMapper(typeof(AppMapperProfile));
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IJwtUtil, JwtUtil>();
builder.Services.AddScoped<ILineNotifyService, LineNotifyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IPlanRepo, PlanRepo>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IDayService, DayService>();
builder.Services.AddScoped<IDayRepo, DayRepo>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IFriendRepo, FriendRepo>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IConfirmEmailService, ConfirmEmailService>();
builder.Services.AddSingleton<IVideoService, VideoService>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddRazorPages().WithRazorPagesRoot("/Pages");
builder.Services.AddControllers()
    .AddJsonOptions(config =>
    {
        config.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        config.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseDefaultFiles();
    app.UseSpaStaticFiles();
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";
    });
}
else if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.MapRazorPages();

app.Run();
