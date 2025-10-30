using DataAccess.Entities;
using DataAccess.Repositories;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    public class ImpactoController : BaseSystemDataController<Impacto>
    {
        public ImpactoController(ISystemData<Impacto> baseSystemData, IAuth auth) : base(baseSystemData, auth)
        {

        }
    }
}
