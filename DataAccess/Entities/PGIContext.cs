using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities;

public partial class PGIContext : DbContext
{
    public PGIContext(DbContextOptions<PGIContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accion> Accions { get; set; }

    public virtual DbSet<Actividade> Actividades { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Areastransversale> Areastransversales { get; set; }

    public virtual DbSet<Auditoria> Auditorias { get; set; }

    public virtual DbSet<Cloudprovider> Cloudproviders { get; set; }

    public virtual DbSet<Compania> Companias { get; set; }

    public virtual DbSet<Credenciale> Credenciales { get; set; }

    public virtual DbSet<Cuentaobjetal> Cuentaobjetals { get; set; }

    public virtual DbSet<Detallesolicitudcompra> Detallesolicitudcompras { get; set; }

    public virtual DbSet<Documentosevidencia> Documentosevidencias { get; set; }

    public virtual DbSet<Documentossolicitudcompra> Documentossolicitudcompras { get; set; }

    public virtual DbSet<Ejesestrategico> Ejesestrategicos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estadoaccione> Estadoacciones { get; set; }

    public virtual DbSet<Estadosolicitud> Estadosolicituds { get; set; }

    public virtual DbSet<Evidencia> Evidencias { get; set; }

    public virtual DbSet<Grupoparametro> Grupoparametros { get; set; }

    public virtual DbSet<Imputacionespresupuestaria> Imputacionespresupuestarias { get; set; }

    public virtual DbSet<Indicador> Indicadors { get; set; }

    public virtual DbSet<Indicadore> Indicadores { get; set; }

    public virtual DbSet<Integracione> Integraciones { get; set; }

    public virtual DbSet<Integracionescredenciale> Integracionescredenciales { get; set; }

    public virtual DbSet<Integracionlog> Integracionlogs { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Objetivo> Objetivos { get; set; }

    public virtual DbSet<Pacc> Paccs { get; set; }

    public virtual DbSet<Parametro> Parametros { get; set; }

    public virtual DbSet<Parametrosvalor> Parametrosvalors { get; set; }

    public virtual DbSet<Pei> Peis { get; set; }

    public virtual DbSet<Periodicidad> Periodicidads { get; set; }

    public virtual DbSet<Poa> Poas { get; set; }

    public virtual DbSet<Productointegracion> Productointegracions { get; set; }

    public virtual DbSet<Profitcenter> Profitcenters { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Proyecto> Proyectoes { get; set; }

    public virtual DbSet<Proyectoindicadore> Proyectoindicadores { get; set; }

    public virtual DbSet<Riesgo> Riesgoes { get; set; }

    public virtual DbSet<Riesgoasociado> Riesgoasociados { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rolmenu> Rolmenus { get; set; }

    public virtual DbSet<Solicitudcompra> Solicitudcompras { get; set; }

    public virtual DbSet<Tipoimpuesto> Tipoimpuestoes { get; set; }

    public virtual DbSet<Tiporiesgo> Tiporiesgoes { get; set; }

    public virtual DbSet<Umbrale> Umbrales { get; set; }

    public virtual DbSet<Unidadmedida> Unidadmedidas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usercompania> Usercompanias { get; set; }

    public virtual DbSet<Userestado> Userestadoes { get; set; }

    public virtual DbSet<Userrole> Userroles { get; set; }

    public virtual DbSet<Xactividade> Xactividades { get; set; }

    public virtual DbSet<Xpacc> Xpaccs { get; set; }

    public virtual DbSet<Xproducto> Xproductos { get; set; }

    public virtual DbSet<Xxactividade> Xxactividades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Accion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("accion");

            entity.HasIndex(e => e.UserId, "FK_Acion_User");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Badge)
                .HasMaxLength(20)
                .HasColumnName("badge");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.EstadoId).HasColumnName("estadoId");
            entity.Property(e => e.NombreCorto)
                .HasMaxLength(20)
                .HasColumnName("nombreCorto");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .HasColumnName("subject");
            entity.Property(e => e.Template)
                .HasMaxLength(1000)
                .HasColumnName("template");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ValidarUsuario).HasColumnName("validarUsuario");

            entity.HasOne(d => d.User).WithMany(p => p.Accions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Acion_User");
        });

        modelBuilder.Entity<Actividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("actividades");

            entity.HasIndex(e => e.UserId, "FK_Actividades_Usuario");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Peso).HasPrecision(15, 2);
            entity.Property(e => e.TipoActividad).HasMaxLength(15);

            entity.HasOne(d => d.User).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actividades_Usuario");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("area");

            entity.Property(e => e.CodigoIntegracion).HasMaxLength(50);
            entity.Property(e => e.CodigoPadre).HasMaxLength(10);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.IdProyecto)
                .HasMaxLength(10)
                .HasColumnName("idProyecto");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Areastransversale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("areastransversales");

            entity.HasIndex(e => e.UserId, "FK_AreasTransversales_Usuario");

            entity.Property(e => e.Created).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Areastransversales)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AreasTransversales_Usuario");
        });

        modelBuilder.Entity<Auditoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("auditoria");

            entity.HasIndex(e => e.Userid, "FK_Auditoria_User");

            entity.Property(e => e.Campo).HasMaxLength(50);
            entity.Property(e => e.ClavePrimaria).HasMaxLength(50);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Host).HasMaxLength(50);
            entity.Property(e => e.Tabla).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Auditorias)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auditoria_User");
        });

        modelBuilder.Entity<Cloudprovider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cloudprovider");

            entity.Property(e => e.AccessKey).HasMaxLength(255);
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.ContainerName).HasMaxLength(255);
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.ProviderType).HasMaxLength(50);
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.SecretKey).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Compania>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("compania");

            entity.HasIndex(e => e.Rnc, "UK_RNC").IsUnique();

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.HoraFinalIntegracion).HasColumnType("time");
            entity.Property(e => e.HoraInicialIntegracion).HasColumnType("time");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Rnc).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Credenciale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("credenciales");

            entity.HasIndex(e => e.UserId, "FK_Credenciales_User");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DbName)
                .HasMaxLength(100)
                .HasColumnName("db_name");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.GeneraToken).HasColumnName("generaToken");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Token).HasMaxLength(100);
            entity.Property(e => e.UrlLogin).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithMany(p => p.Credenciales)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Credenciales_User");
        });

        modelBuilder.Entity<Cuentaobjetal>(entity =>
        {
            entity.HasKey(e => e.Cuenta).HasName("PRIMARY");

            entity.ToTable("cuentaobjetal");

            entity.Property(e => e.Cuenta).HasMaxLength(20);
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Detallesolicitudcompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detallesolicitudcompra");

            entity.HasIndex(e => e.UserId, "FK_DetalleSolicitudCompra_Usuario");

            entity.Property(e => e.Costo).HasPrecision(15, 2);
            entity.Property(e => e.CostoRecepcion).HasPrecision(15, 2);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CuentaObjetal).HasMaxLength(20);
            entity.Property(e => e.Especificaciones).HasMaxLength(8000);
            entity.Property(e => e.FechaAdjudicacion).HasColumnType("datetime");
            entity.Property(e => e.NumeroProceso).HasMaxLength(30);
            entity.Property(e => e.OrdenCompra).HasMaxLength(20);
            entity.Property(e => e.PorcentajeDescuento).HasPrecision(15, 2);
            entity.Property(e => e.ProveedorId).HasMaxLength(13);
            entity.Property(e => e.TipoImpuestoCode).HasMaxLength(10);
            entity.Property(e => e.UmbralCode).HasMaxLength(10);
            entity.Property(e => e.Valor).HasPrecision(15, 2);

            entity.HasOne(d => d.User).WithMany(p => p.Detallesolicitudcompras)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleSolicitudCompra_Usuario");
        });

        modelBuilder.Entity<Documentosevidencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentosevidencias");

            entity.HasIndex(e => e.UserId, "FK_DocumentosEvidencias_Usuario");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(100);
            entity.Property(e => e.TipoArchivo).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Documentosevidencias)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentosEvidencias_Usuario");
        });

        modelBuilder.Entity<Documentossolicitudcompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documentossolicitudcompra");

            entity.HasIndex(e => e.UserId, "FK_DocumentosSolicitudCompra_Usuario");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(100);
            entity.Property(e => e.Paccid).HasColumnName("PACCId");
            entity.Property(e => e.TipoArchivo).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Documentossolicitudcompras)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentosSolicitudCompra_Usuario");
        });

        modelBuilder.Entity<Ejesestrategico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ejesestrategicos");

            entity.HasIndex(e => e.UserId, "FK_EjesEstrategicos_User");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Ejesestrategicoes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EjesEstrategicos_User");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
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
            entity.Property(e => e.Sucursal).HasColumnName("sucursal");
            entity.Property(e => e.Supervisor).HasColumnName("supervisor");
            entity.Property(e => e.Tipo)
                .HasMaxLength(5)
                .HasColumnName("tipo");
            entity.Property(e => e.Turno)
                .HasMaxLength(100)
                .HasColumnName("turno");
        });

        modelBuilder.Entity<Estadoaccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadoacciones");

            entity.HasIndex(e => e.UserId, "FK_EstadoAcciones_Usuario");

            entity.Property(e => e.Created).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Estadoacciones)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadoAcciones_Usuario");
        });

        modelBuilder.Entity<Estadosolicitud>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadosolicitud");

            entity.HasIndex(e => e.UserId, "FK_EstadoSolicitud_User");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("color");
            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Estadosolicituds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadoSolicitud_User");
        });

        modelBuilder.Entity<Evidencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("evidencias");

            entity.HasIndex(e => e.UserId, "FK_Evidencias_Usuario");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Ejecutado).HasPrecision(15, 2);
            entity.Property(e => e.Planificado).HasPrecision(15, 2);

            entity.HasOne(d => d.User).WithMany(p => p.Evidencias)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Evidencias_Usuario");
        });

        modelBuilder.Entity<Grupoparametro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("grupoparametros");

            entity.HasIndex(e => e.Userid, "FK_GrupoParametros_User");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Grupoparametroes)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrupoParametros_User");
        });

        modelBuilder.Entity<Imputacionespresupuestaria>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("imputacionespresupuestarias");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CuentaObjeto).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Indicador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("indicador");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.MedioVerificacion).HasMaxLength(500);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Objetivo)
                .HasPrecision(15, 2)
                .HasColumnName("objetivo");
            entity.Property(e => e.PeriodicidadId).HasColumnName("periodicidadId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.ValorActual)
                .HasPrecision(15, 2)
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
            entity.Property(e => e.Periodicidad).HasMaxLength(50);
            entity.Property(e => e.UnidadMedida).HasMaxLength(50);
            entity.Property(e => e.Áreas).HasMaxLength(500);
        });

        modelBuilder.Entity<Integracione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("integraciones");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<Integracionescredenciale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("integracionescredenciales");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.UltimaEjecucion).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(1000)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Integracionlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("integracionlog");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Existoso).HasColumnName("existoso");
            entity.Property(e => e.Mensaje).HasMaxLength(2000);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Ruta).HasMaxLength(100);
        });

        modelBuilder.Entity<Objetivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("objetivos");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Pacc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pacc");

            entity.Property(e => e.CodigoCatalogo).HasMaxLength(20);
            entity.Property(e => e.CodigoIntegracion).HasMaxLength(20);
            entity.Property(e => e.CostoEstimado).HasPrecision(15, 2);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CuentaObjetal).HasMaxLength(20);
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
        });

        modelBuilder.Entity<Parametro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("parametros");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.ParametroId).HasMaxLength(50);
        });

        modelBuilder.Entity<Parametrosvalor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("parametrosvalor");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(100);
        });

        modelBuilder.Entity<Pei>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pei");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AnoFinal).HasColumnName("anoFinal");
            entity.Property(e => e.AnoInicial).HasColumnName("anoInicial");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasColumnName("descripcion");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Periodicidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("periodicidad");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Poa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poa");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.PeriodoEvidencia).HasColumnName("periodoEvidencia");
            entity.Property(e => e.Planificacion).HasColumnName("planificacion");
        });

        modelBuilder.Entity<Productointegracion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("productointegracion");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.CodigoCatalogo).HasMaxLength(20);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CuentaObjetal).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.InventoryUoMentry)
                .HasMaxLength(10)
                .HasColumnName("InventoryUoMEntry");
            entity.Property(e => e.Itbis).HasMaxLength(10);
        });

        modelBuilder.Entity<Profitcenter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("profitcenters");

            entity.Property(e => e.CenterCode).HasMaxLength(20);
            entity.Property(e => e.InWhichDimension).HasMaxLength(20);
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

            entity.Property(e => e.CodigoIntegracion).HasMaxLength(20);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.FederalTaxId)
                .HasMaxLength(20)
                .HasColumnName("FederalTaxID");
            entity.Property(e => e.RazonSocial).HasMaxLength(200);
            entity.Property(e => e.Rnc).HasMaxLength(15);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyecto");

            entity.Property(e => e.Codigo).HasMaxLength(10);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Dimension1).HasMaxLength(20);
            entity.Property(e => e.Dimension2).HasMaxLength(20);
            entity.Property(e => e.Dimension3).HasMaxLength(20);
            entity.Property(e => e.LineaBase).HasPrecision(15, 2);
            entity.Property(e => e.Meta).HasPrecision(15, 2);
            entity.Property(e => e.Peso).HasPrecision(15, 2);
            entity.Property(e => e.Responsable).HasMaxLength(100);
        });

        modelBuilder.Entity<Proyectoindicadore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectoindicadores");

            entity.Property(e => e.Created).HasColumnType("datetime");
        });

        modelBuilder.Entity<Riesgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("riesgo");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Impacto).HasMaxLength(20);
            entity.Property(e => e.Mitigacion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Origen).HasMaxLength(7);
            entity.Property(e => e.ProbabilidadOcurrencia).HasMaxLength(5);
        });

        modelBuilder.Entity<Riesgoasociado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("riesgoasociados");

            entity.Property(e => e.Created).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Claim).HasMaxLength(100);
            entity.Property(e => e.ClaimValue).HasMaxLength(100);
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Rolmenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rolmenu");

            entity.Property(e => e.Created).HasColumnType("datetime");
        });

        modelBuilder.Entity<Solicitudcompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("solicitudcompra");

            entity.Property(e => e.Codigo).HasMaxLength(255);
        });

        modelBuilder.Entity<Tipoimpuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipoimpuesto");

            entity.Property(e => e.Code).HasMaxLength(15);
            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Rate).HasPrecision(15, 2);
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Tiporiesgo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tiporiesgo");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Badge)
                .HasMaxLength(20)
                .HasColumnName("badge");
            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Umbrale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("umbrales");

            entity.Property(e => e.Code).HasMaxLength(15);
            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Unidadmedida>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("unidadmedida");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UK_Users").IsUnique();

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Usercompania>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usercompania");

            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
        });

        modelBuilder.Entity<Userestado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("userestado");

            entity.Property(e => e.CompaniaId).HasColumnName("companiaId");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
        });

        modelBuilder.Entity<Userrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("userrole");

            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
        });

        modelBuilder.Entity<Xactividade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("xactividades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actividad)
                .HasColumnType("text")
                .HasColumnName("actividad");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .HasColumnName("area");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");
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

            entity.Property(e => e.Actividad).HasMaxLength(255);
            entity.Property(e => e.CodigoIntegracion).HasMaxLength(20);
            entity.Property(e => e.CodigoProyecto).HasMaxLength(20);
            entity.Property(e => e.CuentaObjetal).HasMaxLength(255);
            entity.Property(e => e.CuentaObjetalNormalizada).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.NombreActividad).HasMaxLength(2000);
            entity.Property(e => e.Poa)
                .HasMaxLength(255)
                .HasColumnName("POA");
            entity.Property(e => e.Producto).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(255);
            entity.Property(e => e.UnidMed).HasMaxLength(255);
        });

        modelBuilder.Entity<Xproducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("xproductos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actividad)
                .HasMaxLength(500)
                .HasColumnName("actividad");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .HasColumnName("area");
            entity.Property(e => e.AreaId).HasColumnName("areaId");
            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .HasColumnName("codigo");
            entity.Property(e => e.EjeEstrategico)
                .HasMaxLength(100)
                .HasColumnName("eje_estrategico");
            entity.Property(e => e.Indicador)
                .HasMaxLength(500)
                .HasColumnName("indicador");
            entity.Property(e => e.IndicadorId).HasColumnName("indicadorId");
            entity.Property(e => e.LineaBase).HasColumnName("linea_base");
            entity.Property(e => e.Meta).HasColumnName("meta");
            entity.Property(e => e.ObjetivoEstrategico)
                .HasMaxLength(500)
                .HasColumnName("objetivo_estrategico");
            entity.Property(e => e.ObjetivoId).HasColumnName("objetivoId");
            entity.Property(e => e.Peso)
                .HasPrecision(15, 2)
                .HasColumnName("peso");
            entity.Property(e => e.PesoActividad)
                .HasMaxLength(500)
                .HasColumnName("pesoActividad");
            entity.Property(e => e.PlanificadoAbril)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_abril");
            entity.Property(e => e.PlanificadoAgosto)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_agosto");
            entity.Property(e => e.PlanificadoDiciembre)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_diciembre");
            entity.Property(e => e.PlanificadoEnero)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_enero");
            entity.Property(e => e.PlanificadoFebrero)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_febrero");
            entity.Property(e => e.PlanificadoJulio)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_julio");
            entity.Property(e => e.PlanificadoJunio)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_junio");
            entity.Property(e => e.PlanificadoMarzo)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_marzo");
            entity.Property(e => e.PlanificadoMayo)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_mayo");
            entity.Property(e => e.PlanificadoNoviembre)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_noviembre");
            entity.Property(e => e.PlanificadoOctubre)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_octubre");
            entity.Property(e => e.PlanificadoSeptiembre)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_septiembre");
            entity.Property(e => e.Producto)
                .HasMaxLength(500)
                .HasColumnName("producto");
            entity.Property(e => e.ProyectoId).HasColumnName("proyectoId");
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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actividad)
                .HasMaxLength(500)
                .HasColumnName("actividad");
            entity.Property(e => e.ActividadId).HasColumnName("actividadId");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");
            entity.Property(e => e.Peso)
                .HasMaxLength(10)
                .HasColumnName("peso");
            entity.Property(e => e.PlanificadoAbril)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_abril");
            entity.Property(e => e.PlanificadoAgosto)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_agosto");
            entity.Property(e => e.PlanificadoDiciembre)
                .HasMaxLength(10)
                .HasColumnName("planificado_diciembre");
            entity.Property(e => e.PlanificadoEnero)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_enero");
            entity.Property(e => e.PlanificadoFebrero)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_febrero");
            entity.Property(e => e.PlanificadoJulio)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_julio");
            entity.Property(e => e.PlanificadoJunio)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_junio");
            entity.Property(e => e.PlanificadoMarzo)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_marzo");
            entity.Property(e => e.PlanificadoMayo)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_mayo");
            entity.Property(e => e.PlanificadoNoviembre)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_noviembre");
            entity.Property(e => e.PlanificadoOctubre)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_octubre");
            entity.Property(e => e.PlanificadoSeptiembre)
                .HasPrecision(15, 2)
                .HasColumnName("planificado_septiembre");
            entity.Property(e => e.Producto)
                .HasMaxLength(500)
                .HasColumnName("producto");
            entity.Property(e => e.Proyectoid).HasColumnName("proyectoid");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
