using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

    public interface ISolicitudCompra : IGenericRepo<SolicitudCompra>
{
}


public class SolicitudCompraRepository : GenericRepo<SolicitudCompra>, ISolicitudCompra
{

    public SolicitudCompraRepository(PGIContext context) : base(context)
    {
    }


}
