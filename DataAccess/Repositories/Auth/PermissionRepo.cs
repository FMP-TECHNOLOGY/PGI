using PGI.DataAccess.DbContenxts;
using PGI.DataAccess.Entities;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IPermission : IGenericRepo<Permission>
    {

    }

    public class PermissionRepo : GenericRepo<Permission>, IPermission
    {
        public PermissionRepo(AppDBContext context) : base(context)
        {
        }
    }
}
