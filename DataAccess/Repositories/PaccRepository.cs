using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IPacc : IGenericRepo<Pacc>
{
}


public class PaccRepository : GenericRepo<Pacc>, IPacc
{

    public PaccRepository(PGIContext context) : base(context)
    {
    }


}
