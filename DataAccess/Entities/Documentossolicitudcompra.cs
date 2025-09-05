using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class DocumentosSolicitudCompra
{
    public string? Id { get; set; } 

    public string? SolicitudId { get; set; } 

    public string? Paccid { get; set; } 

    public string? NombreArchivo { get; set; } 

    public string? TipoArchivo { get; set; } 

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
