using DataAccess.Entities.Base;

namespace DataAccess.Entities;

public partial class Accion : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; }

    public string? CompaniaId { get; set; }

    public string? NombreCorto { get; set; }

    public string? Descripcion { get; set; }

    public string? Badge { get; set; }

    public string? Subject { get; set; }

    public string? Template { get; set; }

    public string? EstadoId { get; set; }

    public bool RequiereJustificacion { get; set; }

    public bool ValidarUsuario { get; set; }

    public bool Active { get; set; }

    public string? UserId { get; set; }

    public DateTime Created { get; set; }

    public int? ObjectType { get; }
}
