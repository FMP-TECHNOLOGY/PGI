using System;
using System.Collections.Generic;

namespace DataAccess.Entities;


    public interface IEjesEstrategico : IGenericRepo<EjesEstrategico>
{
}


public class EjesEstrategicoRepository : GenericRepo<EjesEstrategico>, IEjesEstrategico
{

    public EjesEstrategicoRepository(PGIContext context) : base(context)
    {
    }


}
