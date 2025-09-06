using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IMenu : IGenericRepo<Menu>
{
}


public class MenuRepository : GenericRepo<Menu>, IMenu
{

    public MenuRepository(PGIContext context) : base(context)
    {
    }


}
