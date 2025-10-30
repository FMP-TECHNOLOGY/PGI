using DataAccess.Entities;
using DataAccess.Repositories;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    public class GrupoDePaccController : BaseSystemDataController<GrupoDePacc>
    {
        public GrupoDePaccController(ISystemData<GrupoDePacc> baseSystemData, IAuth auth) : base(baseSystemData, auth)
        {

        }
    }
}
