using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Xpacc
{
    public string? CompaniaId { get; set; }
    public string?Poa { get; set; }
    public string?Producto { get; set; }

    public string?Actividad { get; set; }

    public float? CodigoCompra { get; set; }

    public string?Descripcion { get; set; }

    public string?UnidMed { get; set; }

    public float? Enero { get; set; }

    public float? Febrero { get; set; }

    public float? Marzo { get; set; }

    public float? Abril { get; set; }

    public float? Mayo { get; set; }

    public float? Junio { get; set; }

    public float? Julio { get; set; }

    public float? Agosto { get; set; }

    public float? Septiembre { get; set; }

    public float? Octubre { get; set; }

    public float? Noviembre { get; set; }

    public float? Diciembre { get; set; }

    public float? CostoUnitario { get; set; }

    public string?CuentaObjetal { get; set; }

    public string?Tipo { get; set; }

    public string?AreaId { get; set; }

    public string?ProyectoId { get; set; }

    public string?ActividadId { get; set; }

    public string?CodigoIntegracion { get; set; }

    public string?CodigoProyecto { get; set; }

    public string?NombreActividad { get; set; }

    public string? Id { get; set; } 

    public string?CuentaObjetalNormalizada { get; set; }

    public int? ObjectType { get; }
}
