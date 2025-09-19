using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IProyectoRiesgo : IGenericRepo<ProyectoRiesgo>
{
}


public class ProyectoRiesgoRepository : GenericRepo<ProyectoRiesgo>, IProyectoRiesgo
{

    public ProyectoRiesgoRepository(PGIContext context) : base(context)
    {
    }


}
