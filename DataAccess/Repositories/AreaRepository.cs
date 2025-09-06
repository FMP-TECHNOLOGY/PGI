using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IArea : IGenericRepo<Area>
{
}


public class AreaRepository : GenericRepo<Area>, IArea
{

    public AreaRepository(PGIContext context) : base(context)
    {
    }


}
