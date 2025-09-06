using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IProveedor : IGenericRepo<Proveedor>
{
}


public class ProveedorRepository : GenericRepo<Proveedor>, IProveedor
{

    public ProveedorRepository(PGIContext context) : base(context)
    {
    }


}
