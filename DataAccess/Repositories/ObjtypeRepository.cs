using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IObjtype : IGenericRepo<Objtype>
{
}


public class ObjtypeRepository : GenericRepo<Objtype>, IObjtype
{

    public ObjtypeRepository(PGIContext context) : base(context)
    {
    }


}
