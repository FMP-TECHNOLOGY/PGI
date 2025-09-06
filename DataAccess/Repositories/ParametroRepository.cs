using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IParametro : IGenericRepo<Parametro>
{
}


public class ParametroRepository : GenericRepo<Parametro>, IParametro
{

    public ParametroRepository(PGIContext context) : base(context)
    {
    }


}
