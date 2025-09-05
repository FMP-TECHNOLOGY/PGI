using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class DocumentosEvidencia
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? EvidenciaId { get; set; } 

    public string? NombreArchivo { get; set; } 

    public string? TipoArchivo { get; set; } 

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; set; }
}
