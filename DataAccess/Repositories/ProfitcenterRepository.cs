using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;



public interface IProfitCenter : IGenericRepo<ProfitCenter>
{
}


public class ProfitCenterRepository : GenericRepo<ProfitCenter>, IProfitCenter
{

    public ProfitCenterRepository(PGIContext context) : base(context)
    {
    }


}
