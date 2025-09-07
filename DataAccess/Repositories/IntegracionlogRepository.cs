using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IIntegracionLog : IGenericRepo<IntegracionLog>
{
}


public class IntegracionLogRepository : GenericRepo<IntegracionLog>, IIntegracionLog
{

    public IntegracionLogRepository(PGIContext context) : base(context)
    {
    }


}