using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IPoa : IGenericRepo<Poa>
{
}


public class PoaRepository : GenericRepo<Poa>, IPoa
{

    public PoaRepository(PGIContext context) : base(context)
    {
    }


}
