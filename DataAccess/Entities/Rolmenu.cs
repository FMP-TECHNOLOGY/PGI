using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Rolmenu
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public int RolId { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }
}
