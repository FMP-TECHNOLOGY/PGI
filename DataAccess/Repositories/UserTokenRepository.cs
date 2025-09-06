using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

public interface IUserToken : IGenericRepo<UserToken>
{
}


public class UserTokenRepository : GenericRepo<UserToken>, IUserToken
{

    public UserTokenRepository(PGIContext context) : base(context)
    {
    }


}
