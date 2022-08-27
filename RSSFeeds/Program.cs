using Fanap.MarginParkingPlus.Services;
using Fanap.MarginParkingPlus.Services.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RSSFeeds;
using RSSFeeds.Database;
using RSSFeeds.Database.Shared;
using RSSFeeds.Services.Services;
using RSSFeeds.Services.Shared;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddSingleton<IUserManagerService,UserManagerService>();
builder.Services.AddSingleton<IRSSManagerService,RSSManagerService>();
builder.Services.AddSingleton<IRSSUpdaterService, RSSUpdaterService>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<RSSFeedContext>((opts) =>
                   opts.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"), b => b.MigrationsAssembly("RSSFeeds")), ServiceLifetime.Singleton);
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{

    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetSection("JwtSetting").GetValue<string>("SecretKey"))),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});
builder.Services.AddHostedService<Start>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
