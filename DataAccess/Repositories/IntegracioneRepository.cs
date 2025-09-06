using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

 
    public interface IIntegracione : IGenericRepo<Integracione>
{
}


public class IntegracioneRepository : GenericRepo<Integracione>, IIntegracione
{

    public IntegracioneRepository(PGIContext context) : base(context)
    {
    }


}
