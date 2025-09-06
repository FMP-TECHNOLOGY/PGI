using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IUser : IGenericRepo<User>
{
}


public class UserRepository : GenericRepo<User>, IUser
{

    public UserRepository(PGIContext context) : base(context)
    {
    }


}
