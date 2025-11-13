using DataAccess.Entities;

namespace DataAccess.Repositories
{
    // Interfaz
    public interface IEtapaCircuito : IGenericRepo<EtapaCircuito>
    {
    }

    // Implementación
    public class EtapaCircuitoRepository : GenericRepo<EtapaCircuito>, IEtapaCircuito
    {
        public EtapaCircuitoRepository(PGIContext context) : base(context)
        {
        }
    }
}
