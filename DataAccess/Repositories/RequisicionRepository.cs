using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRequisicion : IGenericRepo<Requisicion> { }
    public class RequisicionRepository : GenericRepo<Requisicion>, IRequisicion
    {
        IDetalleRequisicion _DetalleRequisicion;
        public RequisicionRepository(PGIContext context, IDetalleRequisicion detalleRequisicion) : base(context)
        {
            _DetalleRequisicion = detalleRequisicion;
        }

        public override Requisicion AddSaving(Requisicion entity)
        {
            var nuevaRequisicion = base.AddSaving(entity);

            if (entity.Detalles.Count > 0)
            {
                entity.Detalles.ForEach(x => x.RequisicionId = nuevaRequisicion.Id);

                _DetalleRequisicion.AddRangeSaving(entity.Detalles);
            }
            return entity;
        }

    }
}
