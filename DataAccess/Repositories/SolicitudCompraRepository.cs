using DataAccess.Entities;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;
using Utils.Helpers;

namespace DataAccess.Repositories;

public interface ISolicitudCompra : IGenericRepo<SolicitudCompra>
{
}


public class SolicitudCompraRepository : GenericRepo<SolicitudCompra>, ISolicitudCompra
{
    private IDetalleSolicitudCompra _DetalleSolicitudCompra;
    private IAuth Auth;
    public SolicitudCompraRepository(PGIContext context, IDetalleSolicitudCompra detalleSolicitudCompra, IAuth auth) : base(context)
    {
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

                if (entity.DetalleSolicitudCompras.Count == 0) throw new Exception("La solicitud no tiene detalle");
                foreach (var item in entity.DetalleSolicitudCompras)
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

}
