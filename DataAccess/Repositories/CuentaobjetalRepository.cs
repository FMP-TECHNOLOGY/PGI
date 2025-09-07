using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

 
    public interface ICuentaObjetal : IGenericRepo<CuentaObjetal>
{
}


public class CuentaObjetalRepository : GenericRepo<CuentaObjetal>, ICuentaObjetal
{

    public CuentaObjetalRepository(PGIContext context) : base(context)
    {
    }


}
