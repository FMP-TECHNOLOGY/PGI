using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IEstadosolicitud : IGenericRepo<Estadosolicitud>
{
}


public class EstadosolicitudRepository : GenericRepo<Estadosolicitud>, IEstadosolicitud
{

    public EstadosolicitudRepository(PGIContext context) : base(context)
    {
    }


}
