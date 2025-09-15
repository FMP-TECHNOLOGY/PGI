using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IPrograma : IGenericRepo<Programa>
{
}


public class ProgramaRepository : GenericRepo<Programa>, IPrograma
{

    public ProgramaRepository(PGIContext context) : base(context)
    {
    }


}
