using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IRiesgoAsociado : IGenericRepo<RiesgoAsociado>
{
}


public class RiesgoAsociadoRepository : GenericRepo<RiesgoAsociado>, IRiesgoAsociado
{

    public RiesgoAsociadoRepository(PGIContext context) : base(context)
    {
    }


}
