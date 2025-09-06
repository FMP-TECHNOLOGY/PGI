using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

 
    public interface IIntegracionesCredenciale : IGenericRepo<IntegracionesCredenciale>
{
}


public class IntegracionesCredencialeRepository : GenericRepo<IntegracionesCredenciale>, IIntegracionesCredenciale
{

    public IntegracionesCredencialeRepository(PGIContext context) : base(context)
    {
    }


}
