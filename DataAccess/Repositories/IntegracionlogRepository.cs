using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IIntegracionlog : IGenericRepo<Integracionlog>
{
}


public class IntegracionlogRepository : GenericRepo<Integracionlog>, IIntegracionlog
{

    public IntegracionlogRepository(PGIContext context) : base(context)
    {
    }


}