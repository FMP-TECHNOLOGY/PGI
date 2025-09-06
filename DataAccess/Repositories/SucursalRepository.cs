using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface ISucursal : IGenericRepo<Sucursal>
{
}


public class SucursalRepository : GenericRepo<Sucursal>, ISucursal
{

    public SucursalRepository(PGIContext context) : base(context)
    {
    }


}
