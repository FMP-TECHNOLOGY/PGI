using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Documentosevidencia
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int EvidenciaId { get; set; }

    public string? NombreArchivo { get; set; } 

    public string? TipoArchivo { get; set; } 

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
