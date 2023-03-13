using Cinema.Data.Contexts;
using Cinema.Data.Middlewares;
using Cinema.Data.Services.Emails;
using Cinema.Data.Services.JwtTokenServices;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Email"));

#region Services

builder.Services.AddScoped<LoggerMiddleware>();
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddScoped<TransactionMiddleware>();

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<JwtSecurityTokenService>();
builder.Services.AddScoped<CurrentUserService>();

builder.Services.AddScoped<HttpContextAccessor>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddSingleton(Log.Logger);

#endregion

#region Connections

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<DbContext, AppDbContext>();

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

#endregion

#region Jwt bearer

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentications:Issuer"],
            ValidAudience = builder.Configuration["Authentications:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentications:Security"]))
        };
    });

#endregion

#region Authentication policy

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("register", policy => { policy.RequireAuthenticatedUser(); });
});

#endregion

var app = builder.Build();

#region Serilog

app.UseSerilogRequestLogging();

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}

app.UseHttpsRedirection();

#region Authorization and Authentication

app.UseAuthentication();
app.UseAuthorization();

#endregion

#region Middlewares

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggerMiddleware>();
app.UseMiddleware<TransactionMiddleware>();

#endregion

app.MapControllers();

app.Run();
