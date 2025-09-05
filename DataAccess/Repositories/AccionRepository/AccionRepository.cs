using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.AccionRepository
{

    public interface IAccion : IGenericRepo<Accion>
    {
    }


    public class AccionRepository : GenericRepo<Accion>, IAccion
    {

        public AccionRepository(PGIContext context) : base(context)
        {
        }
    

    }
}
