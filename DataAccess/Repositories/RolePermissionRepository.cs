using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

 
    public interface IRolePermission : IGenericRepo<RolePermission>
{
}


public class RolePermissionRepository : GenericRepo<RolePermission>, IRolePermission
{

    public RolePermissionRepository(PGIContext context) : base(context)
    {
    }


}
