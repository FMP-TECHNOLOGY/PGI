using PGI.DataAccess.DbContenxts;
using PGI.DataAccess.Entities;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IRole : IGenericRepo<Role>
    {

    }

    public class RoleRepo : GenericRepo<Role>, IRole
    {
        public RoleRepo(AppDBContext context) : base(context)
        {
        }
    }
}
