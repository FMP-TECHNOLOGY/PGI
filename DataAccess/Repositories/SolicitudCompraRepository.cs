using DataAccess.Entities;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Utils.Helpers;

namespace DataAccess.Repositories;

public interface ISolicitudCompra : IGenericRepo<SolicitudCompra>
{
}


public class SolicitudCompraRepository : GenericRepo<SolicitudCompra>, ISolicitudCompra
{
    private IDetalleSolicitudCompra _DetalleSolicitudCompra;
    private IDocumentosEvidencia _DocumentosEvidencia;
    private IAuth Auth;
    public SolicitudCompraRepository(PGIContext context,
        IDetalleSolicitudCompra detalleSolicitudCompra,
        IDocumentosEvidencia documentosEvidencia,
        IAuth auth) : base(context)
    {
        _DocumentosEvidencia = documentosEvidencia;
        _DetalleSolicitudCompra = detalleSolicitudCompra;
        Auth = auth;
    }


    public override SolicitudCompra AddSaving(SolicitudCompra entity)
    {
        using (var trans = context.Database.BeginTransaction())
        {
            try
            {
                var data = base.Add(entity);

                if (entity.Detalle.Count == 0) throw new Exception("La solicitud no tiene detalle");
                foreach (var item in entity.Detalle)
                {
                    item.SolicitudId = data.Id;
                    if (item.Cantidad == 0) throw new Exception($"No se permiten cantidades en 0");
                    _DetalleSolicitudCompra.Add(item);
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

    public override SolicitudCompra? Find(Expression<Func<SolicitudCompra, bool>> predicate)
    {
        var match = base.Find(predicate);

        if (match == null) return match;

        var detalles = _DetalleSolicitudCompra.FindAll(x => x.SolicitudId == match.Id);

        foreach (var det in detalles)
        {
            det.Files = _DocumentosEvidencia.FindAll(x => x.IdDocumentoBase == det.SolicitudId && x.NoLinea == det.LineNumSolicitud);
        }

        match.Detalle = detalles;
        return match;
    }
}
