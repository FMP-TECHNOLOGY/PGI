using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IProyectoIndicadore : IGenericRepo<ProyectoIndicadore>
{
}


public class ProyectoIndicadoreRepository : GenericRepo<ProyectoIndicadore>, IProyectoIndicadore
{

    public ProyectoIndicadoreRepository(PGIContext context) : base(context)
    {
    }


}
