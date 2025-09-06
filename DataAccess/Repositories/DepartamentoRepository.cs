using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IDepartamento : IGenericRepo<Departamento>
{
}


public class DepartamentoRepository : GenericRepo<Departamento>, IDepartamento
{

    public DepartamentoRepository(PGIContext context) : base(context)
    {
    }


}
