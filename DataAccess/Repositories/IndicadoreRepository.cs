using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IIndicadore : IGenericRepo<Indicadore>
{
}


public class IndicadoreRepository : GenericRepo<Indicadore>, IIndicadore
{

    public IndicadoreRepository(PGIContext context) : base(context)
    {
    }


}
