using DataAccess;
using DataAccess.Entities;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IRole : IGenericRepo<Role>
    {

    }

    public class RoleRepo : GenericRepo<Role>, IRole
    {
        public RoleRepo(PGIContext context) : base(context)
        {
        }
    }
}
