using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IUmbrale : IGenericRepo<Umbrale>
{
}


public class UmbraleRepository : GenericRepo<Umbrale>, IUmbrale
{

    public UmbraleRepository(PGIContext context) : base(context)
    {
    }


}