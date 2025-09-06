using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IProductoIntegracion : IGenericRepo<ProductoIntegracion>
{
}


public class ProductoIntegracionRepository : GenericRepo<ProductoIntegracion>, IProductoIntegracion
{

    public ProductoIntegracionRepository(PGIContext context) : base(context)
    {
    }


}
