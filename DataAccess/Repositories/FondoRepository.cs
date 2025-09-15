using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IFondo : IGenericRepo<Fondo>
{
}


public class FondoRepository : GenericRepo<Fondo>, IFondo
{

    public FondoRepository(PGIContext context) : base(context)
    {
    }


}
