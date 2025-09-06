using DataAccess.Entities;
using System;
using System.Collections.Generic;
using Utils.Helpers;

namespace DataAccess.Repositories;


    public interface ILog : IGenericRepo<Log>
{
}


public class LogRepository : GenericRepo<Log>, ILog
{

    public LogRepository(PGIContext context) : base(context)
    {
    }


}
