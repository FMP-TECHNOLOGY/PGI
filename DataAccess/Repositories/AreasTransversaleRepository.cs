using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IAreasTransversale : IGenericRepo<AreasTransversale>
{
}


public class AreasTransversaleRepository : GenericRepo<AreasTransversale>, IAreasTransversale
{

    public AreasTransversaleRepository(PGIContext context) : base(context)
    {
    }


}
