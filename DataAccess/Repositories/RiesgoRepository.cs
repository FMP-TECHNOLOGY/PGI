using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IRiesgo : IGenericRepo<Riesgo>
{
}


public class RiesgoRepository : GenericRepo<Riesgo>, IRiesgo
{

    public RiesgoRepository(PGIContext context) : base(context)
    {
    }


}
