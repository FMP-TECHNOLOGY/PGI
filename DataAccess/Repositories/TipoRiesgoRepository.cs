using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface ITipoRiesgo : IGenericRepo<TipoRiesgo>
{
}


public class TipoRiesgoRepository : GenericRepo<TipoRiesgo>, ITipoRiesgo
{

    public TipoRiesgoRepository(PGIContext context) : base(context)
    {
    }


}
