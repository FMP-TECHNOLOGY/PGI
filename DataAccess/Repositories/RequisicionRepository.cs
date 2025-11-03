using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRequisicion : IGenericRepo<Requisicion> { }
    public class RequisicionRepository : GenericRepo<Requisicion>, IRequisicion
    {
        public RequisicionRepository(PGIContext context) : base(context)
        {

        }
    }
}
