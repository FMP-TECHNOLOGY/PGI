using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Userrole
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public int IdUser { get; set; }

    public bool Active { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
