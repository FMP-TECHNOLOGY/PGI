using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Empleado
{
    public string? Id { get; set; } 

    public int? Codigo { get; set; }

    public string?Nombres { get; set; }

    public string?Apellidos { get; set; }

    public string?Cedula { get; set; }

    public int? Depto { get; set; }

    public int? Area { get; set; }

    public int? Cargo { get; set; }

    public string?FechaEntrada { get; set; }

    public string?FechaSalida { get; set; }

    public string?Turno { get; set; }

    public string?Tipo { get; set; }

    public string?Estado { get; set; }

    public string?Sexo { get; set; }

    public int? CobraHoras { get; set; }

    public decimal? Salario { get; set; }

    public string?Sucursal { get; set; }

    public decimal? Salariohora { get; set; }

    public string?Rnc { get; set; }

    public string?Rnl { get; set; }

    public int? Supervisor { get; set; }

    public string?Nomina { get; set; }

    public string?DescripcionNomina { get; set; }

    public string? CompaniaId { get; set; }

    public int? ObjectType { get; }
}
