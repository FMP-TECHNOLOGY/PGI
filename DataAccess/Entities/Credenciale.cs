using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Credenciale
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public string? Descripcion { get; set; } 

    public string? Username { get; set; } 

    public string? Password { get; set; } 

    public string? DbName { get; set; } 

    public bool GeneraToken { get; set; }

    public string? UrlLogin { get; set; } 

    public string? Token { get; set; } 

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
