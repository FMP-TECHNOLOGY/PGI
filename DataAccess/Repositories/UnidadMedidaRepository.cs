using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IUnidadMedida : IGenericRepo<UnidadMedida>
{
}


public class UnidadMedidaRepository : GenericRepo<UnidadMedida>, IUnidadMedida
{

    public UnidadMedidaRepository(PGIContext context) : base(context)
    {
    }


}
