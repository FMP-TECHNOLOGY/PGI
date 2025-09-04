using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; } 

    public string? Name { get; set; } 

    public string? Password { get; set; } 

    public int AreaId { get; set; }

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }
    public virtual Compania Compania { get; set; }

    public virtual ICollection<Accion> Accions { get; set; } = new List<Accion>();

    public virtual ICollection<Actividade> Actividades { get; set; } = new List<Actividade>();

    public virtual ICollection<Areastransversale> Areastransversales { get; set; } = new List<Areastransversale>();

    public virtual ICollection<Auditoria> Auditorias { get; set; } = new List<Auditoria>();

    public virtual ICollection<Credenciale> Credenciales { get; set; } = new List<Credenciale>();

    public virtual ICollection<Detallesolicitudcompra> Detallesolicitudcompras { get; set; } = new List<Detallesolicitudcompra>();

    public virtual ICollection<Documentosevidencia> Documentosevidencias { get; set; } = new List<Documentosevidencia>();

    public virtual ICollection<Documentossolicitudcompra> Documentossolicitudcompras { get; set; } = new List<Documentossolicitudcompra>();

    public virtual ICollection<Ejesestrategico> Ejesestrategicoes { get; set; } = new List<Ejesestrategico>();

    public virtual ICollection<Estadoaccione> Estadoacciones { get; set; } = new List<Estadoaccione>();

    public virtual ICollection<Estadosolicitud> Estadosolicituds { get; set; } = new List<Estadosolicitud>();

    public virtual ICollection<Evidencia> Evidencias { get; set; } = new List<Evidencia>();

    public virtual ICollection<Grupoparametro> Grupoparametroes { get; set; } = new List<Grupoparametro>();
}
