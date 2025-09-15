using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IUserDireccionInstitucional : IGenericRepo<UserDireccionInstitucional>
{
}


public class UserDireccionInstitucionalRepository : GenericRepo<UserDireccionInstitucional>, IUserDireccionInstitucional
{

    public UserDireccionInstitucionalRepository(PGIContext context) : base(context)
    {
    }


}
