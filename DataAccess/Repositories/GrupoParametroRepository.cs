using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IGrupoParametro : IGenericRepo<GrupoParametro>
{
}


public class GrupoParametroRepository : GenericRepo<GrupoParametro>, IGrupoParametro
{

    public GrupoParametroRepository(PGIContext context) : base(context)
    {
    }


}