using DataAccess.Entities;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IPei : IGenericRepo<Pei>
{
}


public class PeiRepository : GenericRepo<Pei>, IPei
{
    private IEjesEstrategico _EjesEstrategico;
    private IObjetivo _Objetivo;

    private IAuth Auth;
    public PeiRepository(PGIContext context, IEjesEstrategico ejesEstrategico, IObjetivo objetivo, IAuth auth) : base(context)
    {
        _EjesEstrategico = ejesEstrategico;
        _Objetivo = objetivo;
        Auth = auth;
    }
    public override Pei AddOrUpdateSaving(Pei entity)
    {

        using var trans = context.Database.BeginTransaction();

        try
        {
            var data = base.AddOrUpdate(entity);

            if (entity.EjesEstrategicos.Count > 0)
            {
                foreach (var eje in entity.EjesEstrategicos)
                {
                    eje.PeiId = data.Id;
                    var nuevoEje = _EjesEstrategico.AddOrUpdate(eje);

                    if (eje.Objetivos.Count > 0)
                    {
                        foreach (var objetivo in eje.Objetivos)
                        {
                            objetivo.EjeId = nuevoEje.Id;
                            _Objetivo.AddOrUpdate(objetivo);
                        }
                    }
                }
            }

            base.Save();
            trans.Commit();

            return entity;
        }

        catch (Exception ex)
        {
            trans.Rollback();
            throw new Exception(ex.Message);
        }

    }

}
