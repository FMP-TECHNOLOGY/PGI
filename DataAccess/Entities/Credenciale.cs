using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Credenciale : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; } 

    public string? CompaniaId { get; set; } 

    public string? Descripcion { get; set; } 

    public string? Username { get; set; } 

    public string? Password { get; set; } 

    public string? DbName { get; set; } 

    public bool GeneraToken { get; set; }

    public string? UrlLogin { get; set; } 

    public string? Token { get; set; } 

    public bool Active { get; set; }

    public string?UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
