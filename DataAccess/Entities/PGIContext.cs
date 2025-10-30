using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Common.Exceptions;
using Utils.Extensions;
using Microsoft.Data.SqlClient;
using Utils.Helpers;
using DataAccess.ValueGenerators;

namespace DataAccess.Entities;

public partial class PGIContext : DbContext
{
    private List<Tuple<object, object?, EntityState>> TrackedEntries { get; } = new();

    public virtual DbSet<Log> Logs { get; set; }

    private readonly static Type _longType = typeof(long);
    public PGIContext(DbContextOptions<PGIContext> options)
        : base(options)
    {
        SavingChanges += AppDBContext_SavingChanges;
        SavedChanges += AppDBContext_SavedChanges;
        SaveChangesFailed += AppDBContext_SaveChangesFailed;
    }

    private void AppDBContext_SaveChangesFailed(object? sender, SaveChangesFailedEventArgs e)
    {
        var exception = new BadRequestException(e.Exception);

        var ex =
            e.Exception as SqlException ??
            e.Exception.InnerException as SqlException;

        if (e.Exception is DbUpdateConcurrencyException updateException)
            exception = new BadRequestException($"Data updated by other user. '{string.Join(" | ", updateException.Entries.Select(e => e.Entity?.GetType().Name))}'", updateException);

        if (ex is null)
            goto throwException;

        switch (ex.Number)
        {
            case 2627:
                exception = new BadRequestException(ex.Message, ex);
                break;
        }

    throwException:
        throw exception;
    }

    private void AppDBContext_SavedChanges(object? sender, SavedChangesEventArgs e)
    {
        try
        {
            int trackedCount = TrackedEntries.Count;

            if (trackedCount == 0) return;

            Logs.AddRange(TrackedEntries.Select(x => new Log(x.Item1, x.Item2)
            {
                Action = Enum.GetName(x.Item3) ?? ""
            }));

            TrackedEntries.Clear();

            if (trackedCount > 0)
                SaveChanges();
        }
        catch (Exception ex)
        {
            LogData.Error(ex.Message);
        }
    }

    private void AppDBContext_SavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries().ToList();

        foreach (var entry in entries)
        {
            if (entry.Entity is Log) continue;

            if (!entry.State.In(EntityState.Added, EntityState.Deleted, EntityState.Modified))
                continue;

            if (entry.State == EntityState.Modified)
                GenerateOnUpdate(entry);

            TrackedEntries.Add(new(entry.Entity, entry.GetDatabaseValues()?.ToObject(), entry.State));
        }
    }

    private static void GenerateOnUpdate(EntityEntry entry)
    {
        foreach (var property in entry.Properties)
        {
            if (property.Metadata.IsConcurrencyToken && property.Metadata.ClrType == _longType)
                property.CurrentValue = Convert.ToInt64(property.CurrentValue) + 1;

            if (!property.Metadata.ValueGenerated.In(ValueGenerated.OnUpdate, ValueGenerated.OnAddOrUpdate, ValueGenerated.OnUpdateSometimes))
                continue;

            var valueGeneratorFactory = property.Metadata.GetValueGeneratorFactory();

            if (valueGeneratorFactory == null)
                continue;

            property.CurrentValue = valueGeneratorFactory.Invoke(property.Metadata, entry.Metadata)
                                                         .Next(entry);
        }
    }



    public virtual DbSet<PeriodoEvidencia> PeriodoEvidencias { get; set; }
    public virtual DbSet<Origen> Origenes { get; set; }
    public virtual DbSet<Impacto> Impactos { get; set; }
    public virtual DbSet<ProbabilidadOcurrencia> ProbabilidadOcurrencias { get; set; }
    public virtual DbSet<Accion> Accions { get; set; }

    public virtual DbSet<Actividade> Actividades { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AreasTransversale> Areastransversales { get; set; }

    public virtual DbSet<Auditoria> Auditorias { get; set; }

    public virtual DbSet<CloudProvider> Cloudproviders { get; set; }

    public virtual DbSet<Compania> Companias { get; set; }

    public virtual DbSet<Credenciale> Credenciales { get; set; }

    public virtual DbSet<CuentaObjetal> Cuentaobjetals { get; set; }

    public virtual DbSet<Departamento> Departamentoes { get; set; }
    public virtual DbSet<Fondo> Fondo { get; set; }
    public virtual DbSet<DireccionIntitucional> DireccionInstitucional { get; set; }

    public virtual DbSet<DetalleSolicitudCompra> Detallesolicitudcompras { get; set; }

    public virtual DbSet<DocumentosEvidencia> Documentosevidencias { get; set; }

    public virtual DbSet<DocumentosSolicitudCompra> Documentossolicitudcompras { get; set; }

    public virtual DbSet<EjesEstrategico> Ejesestrategicos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EstadoAccione> Estadoacciones { get; set; }

    public virtual DbSet<EstadoSolicitud> Estadosolicituds { get; set; }

    public virtual DbSet<Evidencia> Evidencias { get; set; }

    public virtual DbSet<GrupoParametro> Grupoparametros { get; set; }

    public virtual DbSet<ImputacionesPresupuestaria> Imputacionespresupuestarias { get; set; }

    public virtual DbSet<Indicador> Indicadors { get; set; }

    public virtual DbSet<Indicadore> Indicadores { get; set; }

    public virtual DbSet<Integracione> Integraciones { get; set; }

    public virtual DbSet<IntegracionesCredenciale> Integracionescredenciales { get; set; }

    public virtual DbSet<IntegracionLog> Integracionlogs { get; set; }

    //public virtual DbSet<Log> Log { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Objetivo> Objetivos { get; set; }

    public virtual DbSet<Objtype> Objtypes { get; set; }

    public virtual DbSet<Pacc> Paccs { get; set; }

    public virtual DbSet<Parametro> Parametros { get; set; }

    public virtual DbSet<ParametrosValor> Parametrosvalors { get; set; }

    public virtual DbSet<Pei> Peis { get; set; }

    public virtual DbSet<Periodicidad> Periodicidads { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Poa> Poas { get; set; }

    public virtual DbSet<ProductoIntegracion> Productointegracions { get; set; }

    public virtual DbSet<ProfitCenter> Profitcenters { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Proyecto> Proyectoes { get; set; }

    public virtual DbSet<ProyectoIndicadore> Proyectoindicadores { get; set; }

    public virtual DbSet<Riesgo> Riesgoes { get; set; }

    public virtual DbSet<ProyectoRiesgo> ProyectoRiesgo { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> Rolepermissions { get; set; }

    public virtual DbSet<RolMenu> Rolmenus { get; set; }

    public virtual DbSet<SolicitudCompra> Solicitudcompras { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TipoImpuesto> Tipoimpuestoes { get; set; }

    public virtual DbSet<TipoRiesgo> Tiporiesgoes { get; set; }

    public virtual DbSet<Umbrale> Umbrales { get; set; }

    public virtual DbSet<UnidadMedida> Unidadmedidas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCompania> UserCompanias { get; set; }
    public virtual DbSet<UserDireccionInstitucional> UserDireccionInstitucional { get; set; }
    public virtual DbSet<UserSucursal> UserSucursal { get; set; }

    public virtual DbSet<UserPermission> Userpermissions { get; set; }

    public virtual DbSet<UserToken> Usertokens { get; set; }

    public virtual DbSet<Xactividade> Xactividades { get; set; }

    public virtual DbSet<Xpacc> Xpaccs { get; set; }

    public virtual DbSet<Xproducto> Xproductos { get; set; }

    public virtual DbSet<Xxactividade> Xxactividades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        ModelDefinitionFrom<ProbabilidadOcurrencia>(modelBuilder, "ProbabilidadOcurrencia");
        ModelDefinitionFrom<Impacto>(modelBuilder, "Impacto");
        ModelDefinitionFrom<Origen>(modelBuilder, "Origen");
        ModelDefinitionFrom<PeriodoEvidencia>(modelBuilder, "PeriodoEvidencia");

        modelBuilder.Entity<Accion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accion");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Badge)
                .HasMaxLength(20)
                .HasColumnName("badge");

            //entity.Property(e => e.CompaniaId)
            //    .HasMaxLength(36)
            //    .HasColumnName("companiaId");

            //entity.Property(e => e.CompaniaId)
            //    .HasColumnName("companiaId")
            //    .HasValueGenerator<CompaniaSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.EstadoId)
                .HasMaxLength(36)
                .HasColumnName("estadoId");
            entity.Property(e => e.NombreCorto)
                .HasMaxLength(20)
                .HasColumnName("nombreCorto");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'1'")
                .HasColumnName("objectType");
            entity.Property(e => e.RequiereJustificacion).HasColumnName("requiereJustificacion");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .HasColumnName("subject");
            entity.Property(e => e.Template)
                .HasMaxLength(1000)
                .HasColumnName("template");
            //entity.Property(e => e.UserId);
            entity.Property(e => e.ValidarUsuario).HasColumnName("validarUsuario");


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<Actividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("actividades");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasColumnName("descripcion");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'2'")
                .HasColumnName("objectType");
            entity.Property(e => e.Orden).HasColumnName("orden");
            entity.Property(e => e.Peso)
                .HasPrecision(19, 2)
                .HasColumnName("peso");
            entity.Property(e => e.ProyectoId)
                .HasMaxLength(36)
                .HasColumnName("proyectoId");
            entity.Property(e => e.TipoActividad)
                .HasMaxLength(15)
                .HasColumnName("tipoActividad");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            //entity.Property(e => e.UserId)
            //    .HasColumnName("userId")
            //    .HasValueGenerator<UserSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("area");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CodigoIntegracion)
                .HasMaxLength(50)
                .HasColumnName("codigoIntegracion");
            entity.Property(e => e.CodigoPadre)
                .HasMaxLength(10)
                .HasColumnName("codigoPadre");
            //entity.Property(e => e.CompaniaId).HasColumnName("companiaId");

            //entity.Property(e => e.CompaniaId)
            //    .HasColumnName("companiaId")
            //    .HasValueGenerator<CompaniaSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            //entity.Property(e => e.Created)
            //    .HasColumnType("datetime")
            //    .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.DepartamentoId)
                .HasMaxLength(36)
                .HasColumnName("departamentoId");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdProyecto)
                .HasMaxLength(10)
                .HasColumnName("idProyecto");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'3'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userid");

            //entity.Property(e => e.UserId)
            //    .HasColumnName("userId")
            //    .HasValueGenerator<UserSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<AreasTransversale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("areastransversales");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.AreaId)
                .HasMaxLength(36)
                .HasColumnName("areaId");
            //entity.Property(e => e.CompaniaId)
            //    .HasMaxLength(36)
            //    .HasColumnName("companiaId");

            //entity.Property(e => e.CompaniaId)
            //    .HasColumnName("companiaId")
            //    .HasValueGenerator<CompaniaSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            //entity.Property(e => e.Created)
            //    .HasColumnType("datetime")
            //    .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'4'")
                .HasColumnName("objectType");
            entity.Property(e => e.ProyectoId)
                .HasMaxLength(36)
                .HasColumnName("proyectoId");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            //entity.Property(e => e.UserId)
            //    .HasColumnName("userId")
            //    .HasValueGenerator<UserSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("auditoria");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Campo)
                .HasMaxLength(50)
                .HasColumnName("campo");
            entity.Property(e => e.ClavePrimaria)
                .HasMaxLength(50)
                .HasColumnName("clavePrimaria");

            entity.Property(e => e.Host)
                .HasMaxLength(50)
                .HasColumnName("host");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'5'")
                .HasColumnName("objectType");
            entity.Property(e => e.Tabla)
                .HasMaxLength(50)
                .HasColumnName("tabla");


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(e => e.ValorActual).HasColumnName("valorActual");
            entity.Property(e => e.ValorAnterior).HasColumnName("valorAnterior");
        });

        modelBuilder.Entity<CloudProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cloudprovider");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.AccessKey)
                .HasMaxLength(255)
                .HasColumnName("accessKey");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");

            entity.Property(e => e.ContainerName)
                .HasMaxLength(255)
                .HasColumnName("containerName");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'6'")
                .HasColumnName("objectType");
            entity.Property(e => e.ProviderType)
                .HasMaxLength(50)
                .HasColumnName("providerType");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.SecretKey)
                .HasMaxLength(255)
                .HasColumnName("secretKey");


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<Compania>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("compania");

            entity.HasIndex(e => e.Rnc, "UK_RNC").IsUnique();

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .HasColumnName("direccion");
            entity.Property(e => e.HoraFinalIntegracion)
                .HasColumnType("time")
                .HasColumnName("horaFinalIntegracion");
            entity.Property(e => e.HoraInicialIntegracion)
                .HasColumnType("time")
                .HasColumnName("horaInicialIntegracion");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'7'")
                .HasColumnName("objectType");
            entity.Property(e => e.Rnc)
                .HasMaxLength(100)
                .HasColumnName("rnc");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            //entity.Property(e => e.UserId)
            //    .HasColumnName("userId")
            //    .HasValueGenerator<UserSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Credenciale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("credenciales");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.DbName)
                .HasMaxLength(100)
                .HasColumnName("db_name");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.GeneraToken).HasColumnName("generaToken");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'8'")
                .HasColumnName("objectType");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .HasColumnName("token");
            entity.Property(e => e.UrlLogin)
                .HasMaxLength(100)
                .HasColumnName("urlLogin");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<CuentaObjetal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cuentaobjetal");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            //entity.Property(e => e.Cuenta).HasColumnName("Cuenta");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'9'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<DireccionIntitucional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("direccion_intitucional");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);


            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.ObjectType)
                .HasColumnName("objectType");
            entity.Property(e => e.Rnc).HasColumnName("Rnc");
            entity.Property(e => e.Active).HasColumnName("Active");
            entity.Property(e => e.Telefono).HasColumnName("Telefono");
            //entity.Property(e => e.UserId).HasMaxLength(45)
            //.HasColumnName("userId");


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => 

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'10'")
                .HasColumnName("objectType");

            //entity.Property(e => e.SucursalId)
            //    .HasMaxLength(36)
            //    .HasColumnName("sucursalId");

            entity.Property(e => e.SucursalId)
                .HasColumnName("sucursalId")
                .HasValueGenerator<SucursalSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Fondo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("fondo");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);

            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.ObjectType)
                .HasColumnName("objectType");
            //entity.Property(e => e.SucursalId)
            //    .HasMaxLength(36)
            //    .HasColumnName("sucursalId");

            entity.Property(e => e.SucursalId)
                .HasColumnName("sucursalId")
                .HasValueGenerator<SucursalSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Active)
                .HasColumnName("Active");


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<DetalleSolicitudCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detallesolicitudcompra");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Costo).HasPrecision(19, 2);
            entity.Property(e => e.CostoRecepcion).HasPrecision(19, 2);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CuentaObjetal)
                .HasMaxLength(36)
                .HasColumnName("cuentaObjetal");
            entity.Property(e => e.Especificaciones)
                .HasColumnType("text")
                .HasColumnName("especificaciones");
            entity.Property(e => e.Estadoid).HasMaxLength(36);
            entity.Property(e => e.FechaAdjudicacion).HasColumnType("datetime");
            entity.Property(e => e.NumeroProceso).HasMaxLength(30);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'11'")
                .HasColumnName("objectType");
            entity.Property(e => e.OrdenCompra).HasMaxLength(20);
            entity.Property(e => e.PaccId)
                .HasMaxLength(36)
                .HasColumnName("paccId");
            entity.Property(e => e.PorcentajeDescuento).HasPrecision(15, 2);
            entity.Property(e => e.ProveedorId).HasMaxLength(36);
            entity.Property(e => e.SolicitudId)
                .HasMaxLength(36)
                .HasColumnName("solicitudId");
            entity.Property(e => e.TipoImpuestoCode).HasMaxLength(10);
            entity.Property(e => e.UmbralCode).HasMaxLength(36);
            entity.Property(e => e.Valor).HasPrecision(19, 2);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<DocumentosEvidencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentosevidencias");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created).HasColumnType("datetime");
            //entity.Property(e => e.EvidenciaId).HasMaxLength(36);
            entity.Property(e => e.NombreArchivo).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'12'")
                .HasColumnName("objectType");
            entity.Property(e => e.TipoArchivo).HasMaxLength(100);


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<DocumentosSolicitudCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentossolicitudcompra");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(100);
            entity.Property(e => e.Paccid)
                .HasMaxLength(36)
                .HasColumnName("PACCId");
            entity.Property(e => e.SolicitudId).HasMaxLength(36);
            entity.Property(e => e.TipoArchivo).HasMaxLength(100);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<EjesEstrategico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ejesestrategicos");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            //entity.Property(e => e.CompaniaId)
            //    .HasColumnName("companiaId")
            //    .HasValueGenerator<CompaniaSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'13'")
                .HasColumnName("objectType");
            entity.Property(e => e.PeiId).HasMaxLength(36);
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd()
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Cargo).HasColumnName("cargo");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .HasColumnName("cedula");
            entity.Property(e => e.CobraHoras).HasColumnName("cobra_horas");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            //entity.Property(e => e.CompaniaId).HasColumnName("companiaId");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Depto).HasColumnName("depto");
            entity.Property(e => e.DescripcionNomina)
                .HasMaxLength(100)
                .HasColumnName("descripcion_nomina");
            entity.Property(e => e.Estado)
                .HasMaxLength(5)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEntrada)
                .HasMaxLength(20)
                .HasColumnName("fecha_entrada");
            entity.Property(e => e.FechaSalida)
                .HasMaxLength(20)
                .HasColumnName("fecha_salida");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.Nomina)
                .HasMaxLength(5)
                .HasColumnName("nomina");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'14'")
                .HasColumnName("objectType");
            entity.Property(e => e.Rnc)
                .HasMaxLength(20)
                .HasColumnName("rnc");
            entity.Property(e => e.Rnl)
                .HasMaxLength(50)
                .HasColumnName("rnl");
            entity.Property(e => e.Salario)
                .HasPrecision(15, 2)
                .HasColumnName("salario");
            entity.Property(e => e.Salariohora)
                .HasPrecision(15, 2)
                .HasColumnName("salariohora");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .HasColumnName("sexo");
            entity.Property(e => e.Sucursal)
                .HasMaxLength(36)
                .HasColumnName("sucursal");
            entity.Property(e => e.Supervisor).HasColumnName("supervisor");
            entity.Property(e => e.Tipo)
                .HasMaxLength(5)
                .HasColumnName("tipo");
            entity.Property(e => e.Turno)
                .HasMaxLength(100)
                .HasColumnName("turno");

        });

        modelBuilder.Entity<EstadoAccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadoacciones");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.AccionId).HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.EstadoId).HasMaxLength(36);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'15'")
                .HasColumnName("objectType");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<EstadoSolicitud>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadosolicitud");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("color");
            //entity.Property(e => e.CompaniaId)
            //    .HasMaxLength(36)
            //    .HasColumnName("companiaId");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'16'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Evidencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("evidencias");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.ActividadId).HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Ejecutado).HasPrecision(15, 2);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'17'")
                .HasColumnName("objectType");
            entity.Property(e => e.Planificado).HasPrecision(15, 2);
            entity.Property(e => e.ProyectoId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<GrupoParametro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("grupoparametros");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'18'")
                .HasColumnName("objectType");

            //entity.Property(e => e.con).IsConcurrencyToken();
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<ImputacionesPresupuestaria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("imputacionespresupuestarias");

            entity.Property(e => e.Active).HasColumnName("active");
            //entity.Property(e => e.Created)
            //    .HasColumnType("datetime")
            //    .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.CuentaObjeto).HasMaxLength(20);
            entity.Property(e => e.CuentaObjetalId).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'19'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Indicador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("indicador");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AreaId).HasMaxLength(36);
            //entity.Property(e => e.CompaniaId)
            //    .HasMaxLength(36)
            //    .HasColumnName("companiaId");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.MedioVerificacion).HasMaxLength(500);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'20'")
                .HasColumnName("objectType");
            entity.Property(e => e.Objetivo)
                .HasPrecision(19, 2)
                .HasColumnName("objetivo");
            entity.Property(e => e.PeriodicidadId)
                .HasMaxLength(36)
                .HasColumnName("periodicidadId");
            entity.Property(e => e.UnidadMedidaId).HasMaxLength(36);
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.ValorActual)
                .HasPrecision(19, 2)
                .HasColumnName("valorActual");
        });

        modelBuilder.Entity<Indicadore>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("indicadores");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.LineaBase)
                .HasMaxLength(50)
                .HasColumnName("Linea Base");
            entity.Property(e => e.MedioVerificacion).HasMaxLength(1000);
            entity.Property(e => e.Meta).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(500);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'21'")
                .HasColumnName("objectType");
            entity.Property(e => e.Periodicidad).HasMaxLength(50);
            entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            entity.Property(e => e.Areas).HasMaxLength(500);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Integracione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("integraciones");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'22'")
                .HasColumnName("objectType");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<IntegracionesCredenciale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("integracionescredenciales");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            //entity.Property(e => e.CompaniaId)
            //    .HasColumnName("companiaId")
            //    .HasValueGenerator<CompaniaSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CredencialId).HasMaxLength(36);
            entity.Property(e => e.IntegracionId).HasMaxLength(36);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'23'")
                .HasColumnName("objectType");
            entity.Property(e => e.UltimaEjecucion).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(1000)
                .HasColumnName("URL");


            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<IntegracionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("integracionlog");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Existoso).HasColumnName("existoso");
            entity.Property(e => e.IntegracionId).HasMaxLength(36);
            entity.Property(e => e.Mensaje).HasMaxLength(2000);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'24'")
                .HasColumnName("objectType");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

        });

        //modelBuilder.Entity<Log>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("PRIMARY");

        //    entity.ToTable("logs");

        //    entity.HasIndex(e => e.CreatedBy, "CreatedBy");

        //    entity.Property(e => e.Action).HasMaxLength(100);
        //    entity.Property(e => e.CreatedBy).HasMaxLength(36);
        //    entity.Property(e => e.IdDocumento).HasMaxLength(36);
        //    entity.Property(e => e.NewData).HasColumnType("json");
        //    entity.Property(e => e.OldData).HasColumnType("json");
        //    entity.Property(e => e.Sha256).HasMaxLength(512);
        //    entity.Property(e => e.Timestamp).HasColumnType("datetime");
        //});

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'25'")
                .HasColumnName("objectType");
            entity.Property(e => e.Ruta).HasMaxLength(100);
        });

        modelBuilder.Entity<Objetivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("objetivos");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            //entity.Property(e => e.CompaniaId)
            //    .HasColumnName("companiaId")
            //    .HasValueGenerator<CompaniaSignValueGenerator>()
            //    .ValueGeneratedOnAdd()
            //    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.EjeId).HasMaxLength(36);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'26'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Objtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("objtype");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Pacc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pacc");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.ActividadId).HasMaxLength(36);
            entity.Property(e => e.CodigoCatalogo).HasMaxLength(20);
            entity.Property(e => e.CodigoIntegracion).HasMaxLength(20);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.CostoEstimado).HasPrecision(19, 2);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CuentaObjetalId).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Grupo).HasMaxLength(30);
            entity.Property(e => e.Mes1).HasPrecision(15, 2);
            entity.Property(e => e.Mes10).HasPrecision(15, 2);
            entity.Property(e => e.Mes11).HasPrecision(15, 2);
            entity.Property(e => e.Mes12).HasPrecision(15, 2);
            entity.Property(e => e.Mes2).HasPrecision(15, 2);
            entity.Property(e => e.Mes3).HasPrecision(15, 2);
            entity.Property(e => e.Mes4).HasPrecision(15, 2);
            entity.Property(e => e.Mes5).HasPrecision(15, 2);
            entity.Property(e => e.Mes6).HasPrecision(15, 2);
            entity.Property(e => e.Mes7).HasPrecision(15, 2);
            entity.Property(e => e.Mes8).HasPrecision(15, 2);
            entity.Property(e => e.Mes9).HasPrecision(15, 2);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'27'")
                .HasColumnName("objectType");
            entity.Property(e => e.PoaId).HasMaxLength(36);
            entity.Property(e => e.ProyectoId).HasMaxLength(36);
        });

        modelBuilder.Entity<Parametro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("parametros");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.GrupoParametroId).HasMaxLength(36);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'28'")
                .HasColumnName("objectType");
            entity.Property(e => e.ParametroId).HasColumnName("ParametroId").HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UserId)
               .HasColumnName("userId")
               .HasValueGenerator<UserSignValueGenerator>()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });

        modelBuilder.Entity<ParametrosValor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("parametrosvalor");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'29'")
                .HasColumnName("objectType");
            entity.Property(e => e.ParametroId).HasMaxLength(36);
            entity.Property(e => e.Value).HasMaxLength(100);
        });

        modelBuilder.Entity<Pei>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pei");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AnoFinal).HasColumnName("anoFinal");
            entity.Property(e => e.AnoInicial).HasColumnName("anoInicial");
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasColumnName("descripcion");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'30'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Periodicidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("periodicidad");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'31'")
                .HasColumnName("objectType");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.ObjectType, "ObjectType");

            entity.HasIndex(e => e.UpdatedBy, "UpdatedBy");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.Description).HasMaxLength(36);
            entity.Property(e => e.LogInstance).HasDefaultValueSql("'1'");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'32'")
                .HasColumnName("objectType");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
        });

        modelBuilder.Entity<Poa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poa");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Ano).HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'33'")
                .HasColumnName("objectType");
            entity.Property(e => e.PeriodoEvidencia).HasColumnName("periodoEvidencia");
            entity.Property(e => e.Planificacion).HasColumnName("planificacion");

            entity.HasOne<PeriodoEvidencia>().WithMany().HasForeignKey(e => e.PeriodoEvidenciaId);
        });

        modelBuilder.Entity<ProductoIntegracion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productointegracion");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.CodigoCatalogo).HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CuentaObjetal).HasMaxLength(36);
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.InventoryUoMentry)
                .HasMaxLength(10)
                .HasColumnName("InventoryUoMEntry");
            entity.Property(e => e.Itbis).HasMaxLength(10);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'34'")
                .HasColumnName("objectType");
        });

        modelBuilder.Entity<ProfitCenter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("profitcenters");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.AreaId).HasMaxLength(36);
            entity.Property(e => e.CenterCode).HasMaxLength(20);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.InWhichDimension).HasMaxLength(20);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'35'")
                .HasColumnName("objectType");
            entity.Property(e => e.Uactividad)
                .HasMaxLength(20)
                .HasColumnName("UActividad");
            entity.Property(e => e.UareaFunc)
                .HasMaxLength(20)
                .HasColumnName("UAreaFunc");
            entity.Property(e => e.Ucodarfun).HasMaxLength(20);
            entity.Property(e => e.Udescripcion)
                .HasMaxLength(300)
                .HasColumnName("UDescripcion");
            entity.Property(e => e.Uproducto)
                .HasMaxLength(20)
                .HasColumnName("UProducto");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedor");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.CodigoIntegracion).HasMaxLength(20);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.FederalTaxId)
                .HasMaxLength(20)
                .HasColumnName("FederalTaxID");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'36'")
                .HasColumnName("objectType");
            entity.Property(e => e.RazonSocial).HasMaxLength(200);
            entity.Property(e => e.Rnc).HasMaxLength(15);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyecto");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.AreaId).HasMaxLength(36);
            entity.Property(e => e.Codigo).HasMaxLength(10);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Dimension1).HasMaxLength(20);
            entity.Property(e => e.Dimension2).HasMaxLength(20);
            entity.Property(e => e.Dimension3).HasMaxLength(20);
            entity.Property(e => e.LineaBase).HasPrecision(19, 2);
            entity.Property(e => e.Meta).HasPrecision(19, 2);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'37'")
                .HasColumnName("objectType");
            //entity.Property(e => e.ObjetivoId).HasMaxLength(36);
            entity.Property(e => e.PeriodicidadId).HasMaxLength(36);
            entity.Property(e => e.Peso).HasPrecision(19, 2);
            entity.Property(e => e.PoaId).HasMaxLength(36);
            entity.Property(e => e.ProgramaId).HasMaxLength(36);
            entity.Property(e => e.FondoId).HasMaxLength(36);
            entity.Property(e => e.Responsable).HasMaxLength(100);
            entity.Property(e => e.UnidadMedidaId).HasMaxLength(36);

            entity.HasOne<UnidadMedida>().WithMany().HasForeignKey(x => x.UnidadMedidaId);

            entity.HasMany<Objetivo>(x=>x.Objetivos).WithMany().UsingEntity<ObjetivoProyeto>(j =>
            {
                j.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
                j.HasOne<Objetivo>().WithMany().HasForeignKey(e => e.ObjetivoId);
                j.HasOne<Proyecto>().WithMany().HasForeignKey(e => e.ProyectoId);
            });
        });

        modelBuilder.Entity<ProyectoIndicadore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectoindicadores");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.IndicadorId).HasMaxLength(36);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'38'")
                .HasColumnName("objectType");
            entity.Property(e => e.ProyectoId).HasMaxLength(36);
        });

        modelBuilder.Entity<Riesgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("riesgo");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Impacto).HasMaxLength(20);
            entity.Property(e => e.Mitigacion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'39'")
                .HasColumnName("objectType");
            entity.Property(e => e.Origen).HasMaxLength(7);
            entity.Property(e => e.ProbabilidadOcurrencia).HasMaxLength(5);

            entity.HasOne<ProbabilidadOcurrencia>().WithMany().HasForeignKey(x => x.ProbabilidadId);
            entity.HasOne<Impacto>().WithMany().HasForeignKey(x => x.ImpactoId);
            entity.HasOne<Origen>().WithMany().HasForeignKey(x => x.OrigenId);

            //entity.HasMany<ProbabilidadOcurrencia>()
            //.WithMany()
            //.UsingEntity<OcurrenciaRiesgo>(j =>
            //{
            //    j.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //    j.HasOne<Riesgo>().WithMany().HasForeignKey(e => e.RiesgoId);
            //    j.HasOne<ProbabilidadOcurrencia>().WithMany().HasForeignKey(e => e.ProbabilidadOcurrenciaId);
            //});

            //entity.HasMany<Impacto>()
            //.WithMany()
            //.UsingEntity<ImpactoRiesgo>(j =>
            //{
            //    j.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //    j.HasOne<Riesgo>().WithMany().HasForeignKey(e => e.RiesgoId);
            //    j.HasOne<Impacto>().WithMany().HasForeignKey(e => e.ImpactoId);
            //});
            //entity.HasMany<Origen>()
            //.WithMany()
            //.UsingEntity<OrigenRiesgo>(j =>
            //{
            //    j.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //    j.HasOne<Riesgo>().WithMany().HasForeignKey(e => e.RiesgoId);
            //    j.HasOne<Origen>().WithMany().HasForeignKey(e => e.OrigenId);
            //});
        });

        modelBuilder.Entity<ProyectoRiesgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectoriesgo");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'40'")
                .HasColumnName("objectType");
            entity.Property(e => e.ProyectoId).HasMaxLength(36);
            entity.Property(e => e.RiesgoId).HasMaxLength(36);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.HasIndex(e => e.ObjectType, "ObjectType");

            entity.HasIndex(e => e.UpdatedBy, "UpdatedBy");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.LogInstance).HasDefaultValueSql("'1'");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'42'")
                .HasColumnName("objectType");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("rolepermissions");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.PermissionId, "PermissionId");

            entity.HasIndex(e => e.UpdatedBy, "UpdatedBy");

            entity.Property(e => e.RoleId).HasMaxLength(36);
            entity.Property(e => e.PermissionId).HasMaxLength(36);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.LogInstance).HasDefaultValueSql("'1'");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'41'")
                .HasColumnName("objectType");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rolmenu");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.MenuId).HasMaxLength(36);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'43'")
                .HasColumnName("objectType");
            entity.Property(e => e.RolId).HasMaxLength(36);
        });

        modelBuilder.Entity<SolicitudCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("solicitudcompra");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Codigo).HasMaxLength(255);
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'44'")
                .HasColumnName("objectType");
            entity.Property(e => e.PoaId).HasMaxLength(36);
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sucursal");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd()
                .HasMaxLength(36)
                .HasColumnName("id");
            //entity.Property(e => e.CompaniaId).HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'45'")
                .HasColumnName("objectType");
            entity.Property(e => e.Rnc)
                .HasMaxLength(45)
                .HasColumnName("rnc");
            //entity.Property(e => e.Userid).HasMaxLength(36);


        });

        modelBuilder.Entity<TipoImpuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipoimpuesto");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Code).HasMaxLength(15);
            //entity.Property(e => e.CompaniaId)
            //    .HasMaxLength(36)
            //    .HasColumnName("companiaId");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'46'")
                .HasColumnName("objectType");
            entity.Property(e => e.Rate).HasPrecision(15, 2);
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<TipoRiesgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tiporiesgo");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Badge)
                .HasMaxLength(20)
                .HasColumnName("badge");
            //entity.Property(e => e.CompaniaId)
            //    .HasMaxLength(36)
            //    .HasColumnName("companiaId");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'47'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Umbrale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("umbrales");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Code).HasMaxLength(15);
            //entity.Property(e => e.CompaniaId)
            //    .HasMaxLength(36)
            //    .HasColumnName("companiaId");

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'48'")
                .HasColumnName("objectType");
            //entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("unidadmedida");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'49'")
                .HasColumnName("objectType");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.ObjectType, "ObjectType");

            entity.HasIndex(e => e.UpdatedBy, "UpdatedBy");

            entity.HasIndex(e => e.Username, "Username").IsUnique();

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Active)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.EmailConfirmed)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.LockoutDueDate).HasColumnType("datetime");
            entity.Property(e => e.LockoutEnabled)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.LogInstance).HasDefaultValueSql("'1'");
            entity.Property(e => e.NationalIdNumber).HasMaxLength(20);
            entity.Property(e => e.NotificationsEnabled)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'52'")
                .HasColumnName("objectType");
            entity.Property(e => e.PasswordExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordExpires)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.PasswordHash).HasColumnType("text");
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.Phone2).HasMaxLength(100);
            entity.Property(e => e.Phone2Confirmed)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.PhoneConfirmed)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.PicturePath).HasColumnType("text");
            entity.Property(e => e.ResetPasswordNextLogin)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.Su)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            //entity.Property(e => e.CompaniaId).HasColumnName("CompaniaId").HasMaxLength(36);

            entity.Property(e => e.CompaniaId)
                .HasColumnName("companiaId")
                .HasValueGenerator<CompaniaSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


            entity.Property(e => e.Username).HasMaxLength(100);

            entity.Property(e => e.Id)
                 .HasValueGenerator<StringGuidValueGenerator>()
                 .ValueGeneratedOnAdd();

            entity.Property(e => e.AccessFailedCount)
                .HasDefaultValueSql("'0'");

            entity.Property(e => e.LogInstance)
                .IsConcurrencyToken();

            entity.Property(e => e.CreatedAt)
                .HasValueGenerator<DateTimeValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.CreatedBy)
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.UpdatedAt)
                .HasValueGenerator<DateTimeValueGenerator>()
                .ValueGeneratedOnUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.UpdatedBy)
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnUpdate()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            //entity.Property(x=>x.CompaniaId)
            //.HasColumnName("")

            entity.HasMany(x => x.Companies)
            .WithMany()
                  .UsingEntity<UserCompania>(j =>
                          j.HasOne(x => x.Company)
                           .WithMany()
                           .HasForeignKey(x => x.CompaniaId),

                          j => j.HasOne(x => x.User)
                                .WithMany()
                                .HasForeignKey(x => x.UserId)
                  );

            //entity.HasMany(x => x.Roles)
            //      .WithMany()
            //      .UsingEntity<UserRole>(j =>
            //              j.HasOne(x => x.Role)
            //               .WithMany()
            //               .HasForeignKey(x => x.RoleId),

            //              j => j.HasOne(x => x.User)
            //                    .WithMany()
            //                    .HasForeignKey(x => x.UserId)
            //);

            entity.HasMany(x => x.Permissions)
                  .WithMany()
                  .UsingEntity<UserPermission>(
                            j => j.HasOne(x => x.Permission)
                                  .WithMany()
                                  .HasForeignKey(x => x.PermissionId),
                            j => j.HasOne(x => x.User)
                                  .WithMany()
                                  .HasForeignKey(x => x.UserId)
                );

            entity.HasOne(x => x.Company)
                  .WithMany()
                  .HasForeignKey(x => x.CompaniaId);

            entity.Navigation(x => x.Company)
                  .AutoInclude();

            entity.Navigation(x => x.Companies)
                  .AutoInclude();

            //entity.Navigation(x => x.Roles)
            //      .AutoInclude();

            entity.Navigation(x => x.Permissions)
                  .AutoInclude();
        });

        modelBuilder.Entity<UserCompania>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercompania");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            //entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'50'")
                .HasColumnName("objectType");

            entity.HasOne(x => x.Company)
                  .WithMany()
                  .HasForeignKey(x => x.CompaniaId);

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<UserDireccionInstitucional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("userdireccioninstitucional");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.DirecionInstitucionalId).HasColumnName("DirecionInstitucionalId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'50'")
                .HasColumnName("objectType");

            entity.HasOne(x => x.DireccionIntitucional)
                  .WithMany()
                  .HasForeignKey(x => x.DirecionInstitucionalId);

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);


            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<UserSucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usersucursal");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.SucursalId).HasColumnName("DirecionInstitucionalId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created").HasValueGenerator<DateTimeValueGenerator>();
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'50'")
                .HasColumnName("objectType");

            entity.HasOne(x => x.Sucursal)
                  .WithMany()
                  .HasForeignKey(x => x.SucursalId);

            entity.HasOne(x => x.User)
                  .WithMany()
                  .HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PRIMARY");

            entity.ToTable("userpermissions");

            //entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            //entity.HasIndex(e => e.PermissionId, "PermissionId");

            //entity.HasIndex(e => e.UpdatedBy, "UpdatedBy");

            entity.Property(e => e.UserId).HasMaxLength(36);
            entity.Property(e => e.PermissionId).HasMaxLength(36);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.LogInstance).HasDefaultValueSql("'1'");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'51'")
                .HasColumnName("objectType");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usertokens");

            entity.HasIndex(e => new { e.AccessToken, e.UserId }, "AccessToken");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.UpdatedBy, "UpdatedBy");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.AccessToken).HasMaxLength(512);
            entity.Property(e => e.Alg).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(36);
            entity.Property(e => e.Exp).HasColumnType("datetime");
            entity.Property(e => e.Hash).HasMaxLength(256);
            entity.Property(e => e.Host).HasMaxLength(256);
            entity.Property(e => e.Jti).HasMaxLength(36);
            entity.Property(e => e.LogInstance).HasDefaultValueSql("'1'");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'53'")
                .HasColumnName("objectType");
            entity.Property(e => e.Typ).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(36);
            //entity.Property(e => e.UserId).HasMaxLength(36);

            entity.Property(e => e.UserId)
                .HasColumnName("userId")
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        });

        modelBuilder.Entity<Xactividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("xactividades");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd()
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.Actividad)
                .HasColumnType("text")
                .HasColumnName("actividad");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .HasColumnName("area");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'54'")
                .HasColumnName("objectType");
            entity.Property(e => e.Peso)
                .HasMaxLength(10)
                .HasColumnName("peso");
            entity.Property(e => e.PlanificadoAbril)
                .HasMaxLength(10)
                .HasColumnName("planificado_abril");
            entity.Property(e => e.PlanificadoAgosto)
                .HasMaxLength(10)
                .HasColumnName("planificado_agosto");
            entity.Property(e => e.PlanificadoDiciembre)
                .HasMaxLength(10)
                .HasColumnName("planificado_diciembre");
            entity.Property(e => e.PlanificadoEnero)
                .HasMaxLength(10)
                .HasColumnName("planificado_enero");
            entity.Property(e => e.PlanificadoFebrero)
                .HasMaxLength(10)
                .HasColumnName("planificado_febrero");
            entity.Property(e => e.PlanificadoJulio)
                .HasMaxLength(10)
                .HasColumnName("planificado_julio");
            entity.Property(e => e.PlanificadoJunio)
                .HasMaxLength(10)
                .HasColumnName("planificado_junio");
            entity.Property(e => e.PlanificadoMarzo)
                .HasMaxLength(10)
                .HasColumnName("planificado_marzo");
            entity.Property(e => e.PlanificadoMayo)
                .HasMaxLength(10)
                .HasColumnName("planificado_mayo");
            entity.Property(e => e.PlanificadoNoviembre)
                .HasMaxLength(10)
                .HasColumnName("planificado_noviembre");
            entity.Property(e => e.PlanificadoOctubre)
                .HasMaxLength(10)
                .HasColumnName("planificado_octubre");
            entity.Property(e => e.PlanificadoSeptiembre)
                .HasMaxLength(10)
                .HasColumnName("planificado_septiembre");
            entity.Property(e => e.Producto)
                .HasColumnType("text")
                .HasColumnName("producto");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Xpacc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("xpacc");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);
            entity.Property(e => e.Actividad).HasMaxLength(255);
            entity.Property(e => e.ActividadId).HasMaxLength(36);
            entity.Property(e => e.AreaId).HasMaxLength(36);
            entity.Property(e => e.CodigoIntegracion).HasMaxLength(20);
            entity.Property(e => e.CodigoProyecto).HasMaxLength(20);
            entity.Property(e => e.CuentaObjetal).HasMaxLength(255);
            entity.Property(e => e.CuentaObjetalNormalizada).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreActividad).HasMaxLength(2000);
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'55'")
                .HasColumnName("objectType");
            entity.Property(e => e.Poa)
                .HasMaxLength(255)
                .HasColumnName("POA");
            entity.Property(e => e.Producto).HasMaxLength(255);
            entity.Property(e => e.ProyectoId).HasMaxLength(36);
            entity.Property(e => e.Tipo).HasMaxLength(255);
            entity.Property(e => e.UnidMed).HasMaxLength(255);
        });

        modelBuilder.Entity<Xproducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("xproductos");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd()
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.Actividad)
                .HasMaxLength(500)
                .HasColumnName("actividad");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .HasColumnName("area");
            entity.Property(e => e.AreaId)
                .HasMaxLength(36)
                .HasColumnName("areaId");
            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .HasColumnName("codigo");
            entity.Property(e => e.EjeEstrategico)
                .HasMaxLength(100)
                .HasColumnName("eje_estrategico");
            entity.Property(e => e.Indicador)
                .HasMaxLength(500)
                .HasColumnName("indicador");
            entity.Property(e => e.IndicadorId)
                .HasMaxLength(36)
                .HasColumnName("indicadorId");
            entity.Property(e => e.LineaBase).HasColumnName("linea_base");
            entity.Property(e => e.Meta).HasColumnName("meta");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'56'")
                .HasColumnName("objectType");
            entity.Property(e => e.ObjetivoEstrategico)
                .HasMaxLength(500)
                .HasColumnName("objetivo_estrategico");
            entity.Property(e => e.ObjetivoId)
                .HasMaxLength(36)
                .HasColumnName("objetivoId");
            entity.Property(e => e.Peso)
                .HasPrecision(15, 2)
                .HasColumnName("peso");
            entity.Property(e => e.PesoActividad)
                .HasMaxLength(500)
                .HasColumnName("pesoActividad");
            entity.Property(e => e.PlanificadoAbril)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_abril");
            entity.Property(e => e.PlanificadoAgosto)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_agosto");
            entity.Property(e => e.PlanificadoDiciembre)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_diciembre");
            entity.Property(e => e.PlanificadoEnero)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_enero");
            entity.Property(e => e.PlanificadoFebrero)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_febrero");
            entity.Property(e => e.PlanificadoJulio)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_julio");
            entity.Property(e => e.PlanificadoJunio)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_junio");
            entity.Property(e => e.PlanificadoMarzo)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_marzo");
            entity.Property(e => e.PlanificadoMayo)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_mayo");
            entity.Property(e => e.PlanificadoNoviembre)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_noviembre");
            entity.Property(e => e.PlanificadoOctubre)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_octubre");
            entity.Property(e => e.PlanificadoSeptiembre)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_septiembre");
            entity.Property(e => e.Producto)
                .HasMaxLength(500)
                .HasColumnName("producto");
            entity.Property(e => e.ProyectoId)
                .HasMaxLength(36)
                .HasColumnName("proyectoId");
            entity.Property(e => e.RiesgosAsociados)
                .HasColumnType("text")
                .HasColumnName("riesgos_asociados");
            entity.Property(e => e.Tipo)
                .HasMaxLength(500)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Xxactividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("xxactividades");

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd()
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.Actividad)
                .HasMaxLength(500)
                .HasColumnName("actividad");
            entity.Property(e => e.ActividadId)
                .HasMaxLength(36)
                .HasColumnName("actividadId");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");
            entity.Property(e => e.ObjectType)
                .HasDefaultValueSql("'57'")
                .HasColumnName("objectType");
            entity.Property(e => e.Peso)
                .HasMaxLength(10)
                .HasColumnName("peso");
            entity.Property(e => e.PlanificadoAbril)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_abril");
            entity.Property(e => e.PlanificadoAgosto)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_agosto");
            entity.Property(e => e.PlanificadoDiciembre)
                .HasMaxLength(10)
                .HasColumnName("planificado_diciembre");
            entity.Property(e => e.PlanificadoEnero)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_enero");
            entity.Property(e => e.PlanificadoFebrero)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_febrero");
            entity.Property(e => e.PlanificadoJulio)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_julio");
            entity.Property(e => e.PlanificadoJunio)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_junio");
            entity.Property(e => e.PlanificadoMarzo)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_marzo");
            entity.Property(e => e.PlanificadoMayo)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_mayo");
            entity.Property(e => e.PlanificadoNoviembre)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_noviembre");
            entity.Property(e => e.PlanificadoOctubre)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_octubre");
            entity.Property(e => e.PlanificadoSeptiembre)
                .HasPrecision(19, 2)
                .HasColumnName("planificado_septiembre");
            entity.Property(e => e.Producto)
                .HasMaxLength(500)
                .HasColumnName("producto");
            entity.Property(e => e.Proyectoid)
                .HasMaxLength(36)
                .HasColumnName("proyectoid");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });


        OnAuditModelCreating(modelBuilder);

        //OnModelCreatingPartial(modelBuilder);
    }

    void ModelDefinitionFrom<T>(ModelBuilder modelBuilder, string? tableName = null) where T : BaseSystemData
    {
        modelBuilder.Entity<T>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            if (!string.IsNullOrWhiteSpace(tableName))
                entity.ToTable(tableName);

            entity.Property(e => e.Id).HasValueGenerator<StringGuidValueGenerator>().ValueGeneratedOnAdd().HasMaxLength(36);

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                //.HasColumnName("createdAt")
                .HasValueGenerator<DateTimeValueGenerator>();
            
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                //.HasColumnName("descripcion")
                ;

            entity.Property(e => e.CreatedBy)
                .HasColumnType("varchar(36)")
                //.HasColumnName("created")
                .HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                //.HasColumnName("createdAt")
                .HasValueGenerator<DateTimeValueGenerator>();

            entity.Property(e => e.UpdatedBy)
                .HasColumnType("varchar(36)")
                //.HasColumnName("created")
                .HasValueGenerator<DateTimeValueGenerator>();
        });

    }

    private static void OnAuditModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Logs");

            entity.HasKey(x => x.Id);

            entity.HasIndex(e => e.CreatedBy);

            entity.Property(e => e.CreatedBy)
                .HasValueGenerator<UserSignValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(e => e.Timestamp)
                .HasValueGenerator<DateTimeValueGenerator>()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        });
    }
    private static void OnViewModelCreating(ModelBuilder modelBuilder)
    {

    }
    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    //private readonly List<Tuple<EntityEntry, EntityState>> trackedEntries = new();



    //private void AppDBContext_SavedChanges(object sender, SavedChangesEventArgs e)
    //{
    //    try
    //    {
    //        int trackedCount = trackedEntries.Count;
    //        if (trackedCount == 0) return;

    //        Logs.AddRange(trackedEntries.Select(x => new Log(JsonConvert.SerializeObject(x.Item1.Entity))
    //        {
    //            Action = Enum.GetName(x.Item2),
    //            //IdDocumento = 
    //            OldData = JsonConvert.SerializeObject(x.Item1.OriginalValues?.ToObject())
    //        }));

    //        trackedEntries.Clear();

    //        if (trackedCount > 0)
    //            SaveChanges();
    //    }
    //    catch { }
    //}

    //private void AppDBContext_SavingChanges(object sender, SavingChangesEventArgs e)
    //{
    //    GenerateOnUpdate();

    //    ChangeTracker.Entries()
    //        .Where(x => x.Entity is not Log && (x.State == EntityState.Added || x.State == EntityState.Deleted || x.State == EntityState.Modified))
    //        .ToList()
    //        .ForEach(entry => trackedEntries.Add(new(entry, entry.State)));
    //}

    //private void GenerateOnUpdate()
    //{
    //    ChangeTracker.Entries().Where(x => x.State == EntityState.Modified)
    //        .ToList()
    //        .ForEach(entry =>
    //        {
    //            entry.Properties.Where(p =>
    //              (p.Metadata.ValueGenerated == ValueGenerated.OnUpdate ||
    //              p.Metadata.ValueGenerated == ValueGenerated.OnAddOrUpdate ||
    //              p.Metadata.ValueGenerated == ValueGenerated.OnUpdateSometimes) &&
    //              p.Metadata.GetValueGeneratorFactory() != null)
    //            .ToList()
    //            .ForEach(p =>
    //            {
    //                p.CurrentValue = p.Metadata
    //                                  .GetValueGeneratorFactory()
    //                                  .Invoke(p.Metadata, entry.Metadata)
    //                                  .Next(entry);
    //            });
    //        });
    //}
}
