using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    // Interfaz
    public interface IAprobadorEtapa : IGenericRepo<AprobadorEtapa>
    {
    }

    // Implementación
    public class AprobadorEtapaRepository : GenericRepo<AprobadorEtapa>, IAprobadorEtapa
    {
        public AprobadorEtapaRepository(PGIContext context) : base(context)
        {
        }
    }
}
