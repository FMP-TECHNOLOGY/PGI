using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IProyectoindicadore : IGenericRepo<Proyectoindicadore>
{
}


public class ProyectoindicadoreRepository : GenericRepo<Proyectoindicadore>, IProyectoindicadore
{

    public ProyectoindicadoreRepository(PGIContext context) : base(context)
    {
    }


}
