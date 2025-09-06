using System;
using System.Collections.Generic;

namespace DataAccess.Entities;


    public interface IEjesestrategico : IGenericRepo<Ejesestrategico>
{
}


public class EjesestrategicoRepository : GenericRepo<Ejesestrategico>, IEjesestrategico
{

    public EjesestrategicoRepository(PGIContext context) : base(context)
    {
    }


}
