using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IEstadoSolicitud : IGenericRepo<EstadoSolicitud>
{
}


public class EstadoSolicitudRepository : GenericRepo<EstadoSolicitud>, IEstadoSolicitud
{

    public EstadoSolicitudRepository(PGIContext context) : base(context)
    {
    }


}
