using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Compania
{
    public int Id { get; set; }

    public string? Name { get; set; } 

    public string? Direccion { get; set; } 

    public string? Telefono { get; set; } 

    public string? Rnc { get; set; } 

    public TimeOnly? HoraInicialIntegracion { get; set; }

    public TimeOnly? HoraFinalIntegracion { get; set; }

    public bool Active { get; set; }

    public int Userid { get; set; }

    public DateTime Created { get; set; }
}
