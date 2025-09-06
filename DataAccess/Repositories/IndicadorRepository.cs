using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IIndicador : IGenericRepo<Indicador>
{
}


public class IndicadorRepository : GenericRepo<Indicador>, IIndicador
{

    public IndicadorRepository(PGIContext context) : base(context)
    {
    }


}
