using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IParametrosValor : IGenericRepo<ParametrosValor>
{
}


public class ParametrosValorRepository : GenericRepo<ParametrosValor>, IParametrosValor
{

    public ParametrosValorRepository(PGIContext context) : base(context)
    {
    }


}
