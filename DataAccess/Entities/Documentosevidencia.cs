using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class DocumentosEvidencia : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? IdDocumentoBase { get; set; } 
    public int? NoLinea { get; set; } 
    public int? ObjectTypeBase { get; set; } 

    public string? NombreArchivo { get; set; } 
    public string? Path { get; set; } 
    public string? Extencion { get; set; } 

    public string? TipoArchivo { get; set; } 

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
