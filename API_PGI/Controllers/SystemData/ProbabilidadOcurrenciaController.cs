using DataAccess.Entities;
using DataAccess.Repositories;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    public class ProbabilidadOcurrenciaController : BaseSystemDataController<ProbabilidadOcurrencia>
    {
        public ProbabilidadOcurrenciaController(ISystemData<ProbabilidadOcurrencia> BaseSystemData, IAuth auth) : base(BaseSystemData, auth)
        {
        }
    }
}
