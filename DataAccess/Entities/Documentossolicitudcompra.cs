using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class DocumentosSolicitudCompra : IUserIdentity, IIdentity
{
    public string? Id { get; set; } 

    public string? SolicitudId { get; set; } 

    public string? Paccid { get; set; } 

    public string? NombreArchivo { get; set; } 

    public string? TipoArchivo { get; set; } 

    public string?UserId { get; set; }

    public DateTime Created { get; set; }
    public int? ObjectType { get; }

}
