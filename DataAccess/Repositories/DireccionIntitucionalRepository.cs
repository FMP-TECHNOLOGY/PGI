using Common.Exceptions;
using DataAccess.Entities;
using ImageMagick;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Repositories
{
    public interface IDireccionIntitucional : IGenericRepo<DireccionIntitucional>
    {
        //IEnumerable<DireccionIntitucional> AddRange(IEnumerable<DireccionIntitucional> entities, string? companiaId = null);


    }
    public class DireccionIntitucionalRepository : GenericRepo<DireccionIntitucional>, IDireccionIntitucional
    {
        ISucursal _Sucursales;
        public DireccionIntitucionalRepository(PGIContext context, ISucursal sucursal) : base(context)
        {
            _Sucursales = sucursal;
        }

        public override IEnumerable<DireccionIntitucional> AddRange(IEnumerable<DireccionIntitucional> entities)
        {
            foreach (var direccion in entities)
            {
                ValidateOrThow(direccion);

                OnCreate(direccion);

                var newDireccion= Add(direccion);

                if(direccion.Sucursales?.Count > 0)
                {
                    direccion.Sucursales.ForEach(x =>
                    {
                        x.DireccionId = newDireccion.Id;
                        x.CompaniaId = direccion.CompaniaId;
                    });

                    _Sucursales.AddRange(direccion.Sucursales);
                }
            }

            return entities;
        }
    }
}
