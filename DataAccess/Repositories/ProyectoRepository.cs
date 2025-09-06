using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

public interface IProyecto : IGenericRepo<Proyecto>
{
}


public class ProyectoRepository : GenericRepo<Proyecto>, IProyecto
{

    public ProyectoRepository(PGIContext context) : base(context)
    {
    }


}
