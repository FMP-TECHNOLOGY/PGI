using DataAccess.Entities;

namespace DataAccess.Repositories
{
    // Interfaz
    public interface ICircuitoDefinicion : IGenericRepo<CircuitoDefinicion>
    {
    }

    // Implementación
    public class CircuitoDefinicionRepository : GenericRepo<CircuitoDefinicion>, ICircuitoDefinicion
    {
        public CircuitoDefinicionRepository(PGIContext context) : base(context)
        {
        }
    }
}
