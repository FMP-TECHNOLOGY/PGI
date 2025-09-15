using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IUserSucursal: IGenericRepo<UserSucursal>
{
}


public class UserSucursalRepository : GenericRepo<UserSucursal>, IUserSucursal
{

    public UserSucursalRepository(PGIContext context) : base(context)
    {
    }


}
