using DataAccess.Entities;

namespace DataAccess.Repositories
{
    // Interfaz
    public interface IDecisionAprobadorEtapa : IGenericRepo<DecisionAprobadorEtapa>
    {
    }

    // Implementación
    public class DecisionAprobadorEtapaRepository : GenericRepo<DecisionAprobadorEtapa>, IDecisionAprobadorEtapa
    {
        public DecisionAprobadorEtapaRepository(PGIContext context) : base(context)
        {
        }
    }
}
