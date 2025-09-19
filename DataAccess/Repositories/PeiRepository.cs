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
    private IAuth Auth;
    public PeiRepository(PGIContext context, IEjesEstrategico ejesEstrategico, IAuth auth) : base(context)
    {
        _EjesEstrategico = ejesEstrategico;
        Auth = auth;
    }
    public override Pei AddOrUpdateSaving(Pei entity)
    {

        using (var trans = context.Database.BeginTransaction())
        {
            try
            {
                var data = base.AddOrUpdateSaving(entity);

                if (entity.EjesEstrategicos.Count > 0)
                {
                    foreach (var item in entity.EjesEstrategicos)
                    {
                        item.CompaniaId = Auth.CurrentCompany.Id;
                        item.PeiId = data.Id;
                        _EjesEstrategico.Add(item);
                    }
                }
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

}
