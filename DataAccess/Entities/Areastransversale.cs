using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Areastransversale
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int ProyectoId { get; set; }

    public int AreaId { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
