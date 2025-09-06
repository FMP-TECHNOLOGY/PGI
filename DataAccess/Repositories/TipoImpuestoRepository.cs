using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

public interface ITipoImpuesto : IGenericRepo<TipoImpuesto>
{
}


public class TipoImpuestoRepository : GenericRepo<TipoImpuesto>, ITipoImpuesto
{

    public TipoImpuestoRepository(PGIContext context) : base(context)
    {
    }


}
