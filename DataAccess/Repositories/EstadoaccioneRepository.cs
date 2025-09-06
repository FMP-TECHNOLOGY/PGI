using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IEstadoaccione : IGenericRepo<Estadoaccione>
{
}


public class EstadoaccioneRepository : GenericRepo<Estadoaccione>, IEstadoaccione
{

    public EstadoaccioneRepository(PGIContext context) : base(context)
    {
    }


}
