using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Estadoaccione
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int EstadoId { get; set; }

    public int AccionId { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public virtual User User { get; set; } 
}
