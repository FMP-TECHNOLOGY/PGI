using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

public interface IXproducto : IGenericRepo<Xproducto>
{
}


public class XproductoRepository : GenericRepo<Xproducto>, IXproducto
{

    public XproductoRepository(PGIContext context) : base(context)
    {
    }


}
