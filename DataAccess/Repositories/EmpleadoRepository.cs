using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IEmpleado : IGenericRepo<Empleado>
{
}


public class EmpleadoRepository : GenericRepo<Empleado>, IEmpleado
{

    public EmpleadoRepository(PGIContext context) : base(context)
    {
    }


}
