using DataAccess.Entities;
using DataAccess.Repositories;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    public class ImpuestoController : BaseSystemDataController<Impuesto>
    {
        public ImpuestoController(ISystemData<Impuesto> baseSystemData, IAuth auth) : base(baseSystemData, auth)
        {

        }
    }
}
