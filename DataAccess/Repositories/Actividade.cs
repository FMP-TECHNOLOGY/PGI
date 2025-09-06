using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IActividade : IGenericRepo<Actividade>
{
}


public class ActividadeRepository : GenericRepo<Actividade>, IActividade
{

    public ActividadeRepository(PGIContext context) : base(context)
    {
    }


}
