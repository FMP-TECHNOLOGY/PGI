using Common.Exceptions;
using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IPacc : IGenericRepo<Pacc>
{
}


public class PaccRepository : GenericRepo<Pacc>, IPacc
{
    IProyecto _Proyecto;
    public PaccRepository(PGIContext context, IProyecto proyecto) : base(context)
    {
        _Proyecto = proyecto;
    }


    public override Pacc AddSaving(Pacc entity)
    {
        if (!_Proyecto.Exists(x => x.Id == entity.ProyectoId))
            throw new CustomException(404, "Proyecto not found");

        var proyect = _Proyecto.Find(x=>x.Id == entity.ProyectoId);
        if (proyect == null)
            throw new CustomException(404, "Proyecto not found");

        entity.AreaId = proyect.AreaId;
        return base.AddSaving(entity);

    }
}
