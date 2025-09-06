using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IPermission : IGenericRepo<Permission>
{
}


public class PermissionRepository : GenericRepo<Permission>, IPermission
{

    public PermissionRepository(PGIContext context) : base(context)
    {
    }


}
