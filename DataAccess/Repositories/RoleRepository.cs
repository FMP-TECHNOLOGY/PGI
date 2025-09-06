using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IRole : IGenericRepo<Role>
{
}


public class RoleRepository : GenericRepo<Role>, IRole
{

    public RoleRepository(PGIContext context) : base(context)
    {
    }


}