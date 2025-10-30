using DataAccess.Entities;
using DataAccess.Repositories;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    public class OrigenController : BaseSystemDataController<Origen>
    {
        public OrigenController(ISystemData<Origen> baseSystemData, IAuth auth) : base(baseSystemData, auth)
        {

        }
    }
}
