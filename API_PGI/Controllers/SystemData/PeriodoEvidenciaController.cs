using DataAccess.Entities;
using DataAccess.Repositories;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    public class PeriodoEvidenciaController : BaseSystemDataController<PeriodoEvidencia>
    {
        public PeriodoEvidenciaController(ISystemData<PeriodoEvidencia> baseSystemData, IAuth auth) : base(baseSystemData, auth)
        {

        }
    }
}
