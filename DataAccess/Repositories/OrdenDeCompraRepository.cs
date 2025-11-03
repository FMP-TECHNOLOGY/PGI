using DataAccess.Entities;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utils.Helpers;

namespace DataAccess.Repositories
{
    public interface IOrdenCompra : IGenericRepo<OrdenCompra>
    {
    }

    public class OrdenDeCompraRepository : GenericRepo<OrdenCompra>, IOrdenCompra
    {

        private IDetalleOrdenCompra _DetalleOrdenCompra;
        private IDocumentosEvidencia _DocumentosEvidencia;
        private IAuth Auth;
        public OrdenDeCompraRepository(PGIContext context,
            IDetalleOrdenCompra detalleOrdenCompra,
            IDocumentosEvidencia documentosEvidencia,
            IAuth auth) : base(context)
        {
            _DocumentosEvidencia = documentosEvidencia;
            _DetalleOrdenCompra = detalleOrdenCompra;
            Auth = auth;
        }


        public override OrdenCompra AddSaving(OrdenCompra entity)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                try
                {
                    var data = base.Add(entity);

                    if (entity.Detalle.Count == 0) throw new Exception("La solicitud no tiene detalle");
                    foreach (var item in entity.Detalle)
                    {
                        item.OrdenCompraId = data.Id;
                        if (item.Cantidad == 0) throw new Exception($"No se permiten cantidades en 0");
                        _DetalleOrdenCompra.Add(item);
                    }
                    base.Save();
                    trans.Commit();
                    return entity;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    LogData.Error(ex);
                    throw;
                }
            }
        }

        public override OrdenCompra? Find(Expression<Func<OrdenCompra, bool>> predicate)
        {
            var match = base.Find(predicate);

            if (match == null) return match;

            var detalles = _DetalleOrdenCompra.FindAll(x => x.OrdenCompraId == match.Id);

            foreach (var det in detalles)
            {
                det.Files = _DocumentosEvidencia.FindAll(x => x.IdDocumentoBase == det.OrdenCompraId && x.NoLinea == det.LineNumOrden);
            }

            match.Detalle = detalles;
            return match;
        }


    }
}
