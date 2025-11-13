using DataAccess.Entities;
namespace DataAccess.Repositories
{
    // Interfaz
    public interface IEtapaCircuitoEjecucion : IGenericRepo<EtapaCircuitoEjecucion>
    {
    }

    // Implementación
    public class EtapaCircuitoEjecucionRepository : GenericRepo<EtapaCircuitoEjecucion>, IEtapaCircuitoEjecucion
    {
        public EtapaCircuitoEjecucionRepository(PGIContext context) : base(context)
        {
        }
    }
}
