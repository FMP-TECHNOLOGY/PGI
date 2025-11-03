using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IDetallePacc : IGenericRepo<DetallePacc> { }
    public class DetallePaccRepository : GenericRepo<DetallePacc>, IDetallePacc
    {
        public DetallePaccRepository(PGIContext context) : base(context)
        {

        }
    }
}
