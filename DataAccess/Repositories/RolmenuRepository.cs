using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

    public interface IRolmenu : IGenericRepo<Rolmenu>
{
}


public class RolmenuRepository : GenericRepo<Rolmenu>, IRolmenu
{

    public RolmenuRepository(PGIContext context) : base(context)
    {
    }


}
