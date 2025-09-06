using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IUserPermission : IGenericRepo<UserPermission>
{
}


public class UserPermissionRepository : GenericRepo<UserPermission>, IUserPermission
{

    public UserPermissionRepository(PGIContext context) : base(context)
    {
    }


}
