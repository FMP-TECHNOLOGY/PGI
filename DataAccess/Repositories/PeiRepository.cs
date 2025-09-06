using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IPei : IGenericRepo<Pei>
{
}


public class PeiRepository : GenericRepo<Pei>, IPei
{

    public PeiRepository(PGIContext context) : base(context)
    {
    }


}
