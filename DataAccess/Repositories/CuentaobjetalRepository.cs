using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

 
    public interface ICuentaobjetal : IGenericRepo<Cuentaobjetal>
{
}


public class CuentaobjetalRepository : GenericRepo<Cuentaobjetal>, ICuentaobjetal
{

    public CuentaobjetalRepository(PGIContext context) : base(context)
    {
    }


}
