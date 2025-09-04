using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Userestado
{
    public int Id { get; set; }

    public int CompaniaId { get; set; }

    public int ValidUser { get; set; }

    public int EstadoId { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
