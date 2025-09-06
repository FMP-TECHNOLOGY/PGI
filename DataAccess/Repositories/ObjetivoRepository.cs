using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IObjetivo : IGenericRepo<Objetivo>
{
}


public class ObjetivoRepository : GenericRepo<Objetivo>, IObjetivo
{

    public ObjetivoRepository(PGIContext context) : base(context)
    {
    }


}
