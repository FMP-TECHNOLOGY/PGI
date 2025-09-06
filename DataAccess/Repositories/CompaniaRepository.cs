using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface ICompania : IGenericRepo<Compania>
{
}


public class CompaniaRepository : GenericRepo<Compania>, ICompania
{

    public CompaniaRepository(PGIContext context) : base(context)
    {
    }


}