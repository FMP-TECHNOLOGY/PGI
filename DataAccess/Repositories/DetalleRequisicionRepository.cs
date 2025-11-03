using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IDetalleRequisicion : IGenericRepo<DetalleRequisicion> { }
    public class DetalleRequisicionRepository : GenericRepo<DetalleRequisicion>, IDetalleRequisicion
    {
        public DetalleRequisicionRepository(PGIContext context) : base(context)
        {

        }
    }
}
