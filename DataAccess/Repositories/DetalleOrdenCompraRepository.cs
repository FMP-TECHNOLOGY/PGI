using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IDetalleOrdenCompra : IGenericRepo<DetalleOrdenCompra> { }
    public class DetalleOrdenCompraRepository : GenericRepo<DetalleOrdenCompra>, IDetalleOrdenCompra
    {
        public DetalleOrdenCompraRepository(PGIContext context) : base(context)
        {

        }
    }
}
