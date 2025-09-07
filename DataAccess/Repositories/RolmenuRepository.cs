using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

    public interface IRolMenu : IGenericRepo<RolMenu>
{
}


public class RolMenuRepository : GenericRepo<RolMenu>, IRolMenu
{

    public RolMenuRepository(PGIContext context) : base(context)
    {
    }


}
