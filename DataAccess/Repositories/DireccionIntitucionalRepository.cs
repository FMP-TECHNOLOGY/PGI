using DataAccess.Entities;
using System;

namespace DataAccess.Repositories
{
    public interface IDireccionIntitucional : IGenericRepo<DireccionIntitucional>
    {

    }
    public class DireccionIntitucionalRepository : GenericRepo<DireccionIntitucional>
    {
        public DireccionIntitucionalRepository(PGIContext context) : base(context)
        {

        }
    }
}
