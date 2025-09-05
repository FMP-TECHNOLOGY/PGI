using API_PGI.Auth;
using Auth.JWT;
using DataAccess.Entities;
using DataAccess.Repositories.AccionRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_CONFIG", builder =>
    {
        builder
            //.AllowCredentials()
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();

    });

});
// CultureInfo
builder.Services.AddRequestLocalization(opt =>
{
    var defaultCultureInfo = new CultureInfo("es-DO")
    {
        DateTimeFormat = new DateTimeFormatInfo()
        {
            DateSeparator = "-",
            TimeSeparator = ":"
        },
        NumberFormat = new NumberFormatInfo()
        {
            NumberDecimalSeparator = ".",
            CurrencySymbol = "$"
        }
    };

    opt.SupportedCultures = new[] { defaultCultureInfo };
    opt.ApplyCurrentCultureToResponseHeaders = true;
    opt.SetDefaultCulture("es-DO");
});
// JSON Web Token Configuration File
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWT"));

// JSON Web Token Authentication Middleware
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

});

// JSON Web Token Authorization
builder.Services.AddAuthorization(auth =>
{
    auth.DefaultPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portal de Gestión Institucional", Version = "v1" });

    var security = new OpenApiSecurityScheme()
    {
        Description = "Ingrese la palabra 'Bearer' seguida por un espacio y su token. Ex: Bearer eyJhbGciOcCI6IkpXVCJ9...",
        Name = "Authorization",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        },
    };

    c.AddSecurityDefinition(security.Reference.Id, security);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { security, Array.Empty<string>() }
                });
});

//Interfaces
builder.Services.AddScoped<IAccion, AccionRepository>();



builder.Services.AddDbContext<PGIContext>(x =>
{
    var dbConnectionString = builder.Configuration.GetConnectionString("MySql");
    x.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString), p => { p.CommandTimeout(60); });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseReDoc(c =>
{
    c.DocumentTitle = "REDOC API Documentation";
    c.SpecUrl = "/swagger/v1/swagger.json";

});
app.UseCors("CORS_CONFIG");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.UseMiddleware<AuthMiddleware>();


app.MapControllers();

app.Run();
