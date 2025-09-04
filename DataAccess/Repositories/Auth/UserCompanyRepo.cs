using PGI.DataAccess.DbContenxts;
using PGI.DataAccess.Entities;

namespace PGI.DataAccess.Repositories.Auth
{
    public interface IUserCompany : IGenericRepo<UserCompany>
    {

    }

    public class UserCompanyRepo : GenericRepo<UserCompany>, IUserCompany
    {
        public UserCompanyRepo(AppDBContext context) : base(context)
        {
        }
    }
}
