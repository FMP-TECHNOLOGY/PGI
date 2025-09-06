using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface ICredenciale : IGenericRepo<Credenciale>
{
}


public class CredencialeRepository : GenericRepo<Credenciale>, ICredenciale
{

    public CredencialeRepository(PGIContext context) : base(context)
    {
    }


}
