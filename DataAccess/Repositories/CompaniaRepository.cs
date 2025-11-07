using Common.Exceptions;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Repositories;


    public interface ICompania : IGenericRepo<Compania>
{
}


public class CompaniaRepository : GenericRepo<Compania>, ICompania
{
    IDireccionIntitucional _DireccionIntitucional;
    public CompaniaRepository(PGIContext context, IDireccionIntitucional direccionIntitucional) : base(context)
    {
        _DireccionIntitucional = direccionIntitucional;
    }

    public override Compania AddSaving(Compania entity)
    {

        ValidateOrThow(entity);

        OnCreate(entity);

        using var trans = context.Database.BeginTransaction();
        try
        {
            var newCompany = Add(entity);

            if (entity.Direcciones?.Count > 0)
            {
                entity.Direcciones.ForEach(x => x.CompaniaId = newCompany.Id);

                _DireccionIntitucional.AddRange(entity.Direcciones);
            }

            Save();

            trans.Commit();

        }
        catch 
        {
            trans.Rollback();
            throw;
        }
        finally
        {
            trans.Dispose();
        }

        OnCreated(entity);

        return entity;
    }

}