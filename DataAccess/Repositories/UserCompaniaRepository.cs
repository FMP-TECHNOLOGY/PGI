using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

public interface IUserCompania : IGenericRepo<UserCompania>
{
}


public class UserCompaniaRepository : GenericRepo<UserCompania>, IUserCompania
{

    public UserCompaniaRepository(PGIContext context) : base(context)
    {
    }


}
