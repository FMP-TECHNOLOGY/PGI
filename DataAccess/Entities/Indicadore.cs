using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Indicadore
{
    public string? Áreas { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? UnidadMedida { get; set; }

    public string? Periodicidad { get; set; }

    public string? LineaBase { get; set; }

    public string? Meta { get; set; }

    public string? MedioVerificacion { get; set; }
}
