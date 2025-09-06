using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IDetalleSolicitudCompra : IGenericRepo<DetalleSolicitudCompra>
{
}


public class DetalleSolicitudCompraRepository : GenericRepo<DetalleSolicitudCompra>, IDetalleSolicitudCompra
{

    public DetalleSolicitudCompraRepository(PGIContext context) : base(context)
    {
    }


}