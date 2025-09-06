using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

public interface IXpacc : IGenericRepo<Xpacc>
{
}


public class XpaccRepository : GenericRepo<Xpacc>, IXpacc
{

    public XpaccRepository(PGIContext context) : base(context)
    {
    }


}
