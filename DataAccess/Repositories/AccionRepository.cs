using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IAccion : IGenericRepo<Accion>
{
}


public class AccionRepository : GenericRepo<Accion>, IAccion
{

    public AccionRepository(PGIContext context) : base(context)
    {
    }


}
