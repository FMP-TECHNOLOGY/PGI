using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Indicadore
{
    public string? Id { get; set; }
    public string? Areas { get; set; }

    public string?Nombre { get; set; }

    public string?Descripcion { get; set; }

    public string?UnidadMedida { get; set; }

    public string?Periodicidad { get; set; }

    public string?LineaBase { get; set; }

    public string?Meta { get; set; }

    public string?MedioVerificacion { get; set; }

    public Guid? CompaniaId { get; set; }
    public int? ObjectType { get; set; }
}
