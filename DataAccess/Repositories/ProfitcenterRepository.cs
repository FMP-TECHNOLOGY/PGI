using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;



public interface IProfitcenter : IGenericRepo<Profitcenter>
{
}


public class ProfitcenterRepository : GenericRepo<Profitcenter>, IProfitcenter
{

    public ProfitcenterRepository(PGIContext context) : base(context)
    {
    }


}
