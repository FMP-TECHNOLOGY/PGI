using DataAccess.Entities;

namespace DataAccess.Repositories
{
    // Interfaz
    public interface ICircuitoEjecucion : IGenericRepo<CircuitoEjecucion>
    {
    }

    // Implementación
    public class CircuitoEjecucionRepository : GenericRepo<CircuitoEjecucion>, ICircuitoEjecucion
    {
        public CircuitoEjecucionRepository(PGIContext context) : base(context)
        {
        }
    }
}
