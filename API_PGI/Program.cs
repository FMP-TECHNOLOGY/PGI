using API_PGI;
using API_PGI.Auth;
using API_PGI.Exceptions;
using Auth.JWT;
using Common.Exceptions;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Gridify;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ExceptionHandlerFilter));
});

builder.Services.AddEndpointsApiExplorer();

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
builder.Services.AddScoped<IAuth, AuthRepo>();

builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IUserToken, UserTokenRepo>();
builder.Services.AddScoped<IUserCompany, UserCompanyRepo>();
builder.Services.AddScoped<IRole, RoleRepo>();
builder.Services.AddScoped<IPermission, PermissionRepo>();
// IUserApiKey userApiKeys,
builder.Services.AddScoped<IRolePermission, RolePermissionRepo>();
builder.Services.AddScoped<IUserPermission, UserPermissionRepo>();

builder.Services.AddScoped<IAccion, AccionRepository>();
builder.Services.AddScoped<IActividade, ActividadeRepository>();
builder.Services.AddScoped<IArea, AreaRepository>();
builder.Services.AddScoped<IAreasTransversale, AreasTransversaleRepository>();
builder.Services.AddScoped<IAuditoria, AuditoriaRepository>();
builder.Services.AddScoped<ICloudProvider, CloudProviderRepository>();
builder.Services.AddScoped<ICompania, CompaniaRepository>();
builder.Services.AddScoped<ICredenciale, CredencialeRepository>();
builder.Services.AddScoped<ICuentaObjetal, CuentaObjetalRepository>();
builder.Services.AddScoped<IDepartamento, DepartamentoRepository>();
builder.Services.AddScoped<IDetalleSolicitudCompra, DetalleSolicitudCompraRepository>();
builder.Services.AddScoped<IDocumentosEvidencia, DocumentosEvidenciaRepository>();
builder.Services.AddScoped<IDocumentosSolicitudCompra, DocumentosSolicitudCompraRepository>();
builder.Services.AddScoped<IEjesEstrategico, EjesEstrategicoRepository>();
builder.Services.AddScoped<IEmpleado, EmpleadoRepository>();
builder.Services.AddScoped<IEstadoAccione, EstadoAccioneRepository>();
builder.Services.AddScoped<IEstadoSolicitud, EstadoSolicitudRepository>();
builder.Services.AddScoped<IEvidencia, EvidenciaRepository>();
builder.Services.AddScoped<IGrupoParametro, GrupoParametroRepository>();
builder.Services.AddScoped<IImputacionesPresupuestaria, ImputacionesPresupuestariaRepository>();
builder.Services.AddScoped<IIndicador, IndicadorRepository>();
builder.Services.AddScoped<IIndicadore, IndicadoreRepository>();
builder.Services.AddScoped<IIntegracione, IntegracioneRepository>();
builder.Services.AddScoped<IIntegracionesCredenciale, IntegracionesCredencialeRepository>();
builder.Services.AddScoped<IIntegracionLog, IntegracionLogRepository>();
builder.Services.AddScoped<ILog, LogRepository>();
builder.Services.AddScoped<IMenu, MenuRepository>();
builder.Services.AddScoped<IObjetivo, ObjetivoRepository>();
builder.Services.AddScoped<IObjtype, ObjtypeRepository>();
builder.Services.AddScoped<IPacc, PaccRepository>();
builder.Services.AddScoped<IParametro, ParametroRepository>();
builder.Services.AddScoped<IParametrosValor, ParametrosValorRepository>();
builder.Services.AddScoped<IPei, PeiRepository>();
builder.Services.AddScoped<IPeriodicidad, PeriodicidadRepository>();
builder.Services.AddScoped<IPermission, PermissionRepo>();
builder.Services.AddScoped<IPoa, PoaRepository>();
builder.Services.AddScoped<IPrograma, ProgramaRepository>();
builder.Services.AddScoped<IFondo,FondoRepository>();
builder.Services.AddScoped<IUserSucursal,UserSucursalRepository>();
builder.Services.AddScoped<IUserDireccionInstitucional, UserDireccionInstitucionalRepository>();
builder.Services.AddScoped<IProductoIntegracion, ProductoIntegracionRepository>();
builder.Services.AddScoped<IProfitCenter, ProfitCenterRepository>();
builder.Services.AddScoped<IProveedor, ProveedorRepository>();
builder.Services.AddScoped<IProyecto, ProyectoRepository>();
builder.Services.AddScoped<IProyectoIndicadore, ProyectoIndicadoreRepository>();
builder.Services.AddScoped<IRiesgo, RiesgoRepository>();
builder.Services.AddScoped<IProyectoRiesgo, ProyectoRiesgoRepository>();
builder.Services.AddScoped<IRole, RoleRepo>();
builder.Services.AddScoped<IRolePermission, RolePermissionRepo>();
builder.Services.AddScoped<IRolMenu, RolMenuRepository>();
builder.Services.AddScoped<ISolicitudCompra, SolicitudCompraRepository>();
builder.Services.AddScoped<ISucursal, SucursalRepository>();
builder.Services.AddScoped<ITipoImpuesto, TipoImpuestoRepository>();
builder.Services.AddScoped<ITipoRiesgo, TipoRiesgoRepository>();
builder.Services.AddScoped<IUmbrale, UmbraleRepository>();
builder.Services.AddScoped<IUnidadMedida, UnidadMedidaRepository>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IUserCompania, UserCompaniaRepository>();
builder.Services.AddScoped<IUserPermission, UserPermissionRepo>();
builder.Services.AddScoped<IUserToken, UserTokenRepo>();
builder.Services.AddScoped<IXactividade, XactividadeRepository>();
builder.Services.AddScoped<IXpacc, XpaccRepository>();
builder.Services.AddScoped<IXproducto, XproductoRepository>();
builder.Services.AddScoped<IXxactividade, XxactividadeRepository>();
builder.Services.AddScoped<IGlobalFilters, GlobalFilters>();


builder.Services.AddScoped<IDireccionIntitucional, DireccionIntitucionalRepository>();
builder.Services.AddScoped<IDetalleRequisicion, DetalleRequisicionRepository>();
builder.Services.AddScoped<IRequisicion, RequisicionRepository>();


GridifyGlobalConfiguration.DefaultPageSize = int.MaxValue;



builder.Services.AddDbContext<PGIContext>(x =>
{
    var dbConnectionString = builder.Configuration.GetConnectionString("MySql");
    x.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString), p => { p.CommandTimeout(60); });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.Use((context, next) =>
{
    if (!"GET".Equals(context.Request.Method, StringComparison.InvariantCultureIgnoreCase))
        goto next;

    var globals = context.RequestServices.GetService<IGlobalFilters>();
    if (globals is null)
        goto next;

    if (!context.Request.Query.TryGetValue("Expand", out var filters))
        goto next;

    var value = filters.ToString();

    if (!bool.TryParse(value, out var expand))
        throw new BadRequestException($"The value {value} for the 'Expand' query parameter is not a valid boolean value");

    globals.Expand = expand;

next:
    return next(context);
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseReDoc(c =>
//{
//    c.DocumentTitle = "REDOC API Documentation";
//    c.SpecUrl = "/swagger/v1/swagger.json";

//});
app.UseCors("CORS_CONFIG");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<AuthMiddleware>();

app.UseStaticFiles();

app.MapControllers();

app.Run();
