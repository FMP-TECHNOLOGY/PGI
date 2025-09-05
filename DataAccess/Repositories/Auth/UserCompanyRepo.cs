

using DataAccess;
using DataAccess.Entities;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IUserCompany : IGenericRepo<UserCompania>
    {

    }

    public class UserCompanyRepo : GenericRepo<UserCompania>, IUserCompany
    {
        public UserCompanyRepo(PGIContext context) : base(context)
        {
        }
    }
}
