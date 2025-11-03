using DataAccess.Entities;
using DataAccess.Repositories;
using PGI.DataAccess.Repositories.Auth;

namespace API_PGI.Controllers.SystemData
{
    public class TipoUmbralController : BaseSystemDataController<TipoUmbral>
    {
        public TipoUmbralController(ISystemData<TipoUmbral> baseSystemData, IAuth auth) : base(baseSystemData, auth)
        {

        }
    }
}
