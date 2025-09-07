using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IEstadoAccione : IGenericRepo<EstadoAccione>
{
}


public class EstadoAccioneRepository : GenericRepo<EstadoAccione>, IEstadoAccione
{

    public EstadoAccioneRepository(PGIContext context) : base(context)
    {
    }


}
