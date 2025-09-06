using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IImputacionesPresupuestaria : IGenericRepo<ImputacionesPresupuestaria>
{
}


public class ImputacionesPresupuestariaRepository : GenericRepo<ImputacionesPresupuestaria>, IImputacionesPresupuestaria
{

    public ImputacionesPresupuestariaRepository(PGIContext context) : base(context)
    {
    }


}