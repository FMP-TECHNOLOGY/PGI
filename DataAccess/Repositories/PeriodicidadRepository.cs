using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IPeriodicidad : IGenericRepo<Periodicidad>
{
}


public class PeriodicidadRepository : GenericRepo<Periodicidad>, IPeriodicidad
{

    public PeriodicidadRepository(PGIContext context) : base(context)
    {
    }


}
