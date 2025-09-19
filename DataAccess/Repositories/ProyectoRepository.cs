using DataAccess.Entities;
using PGI.DataAccess.Repositories.Auth;
using System;
using System.Collections.Generic;
using Utils.Helpers;

namespace DataAccess.Repositories;

public interface IProyecto : IGenericRepo<Proyecto>
{
}


public class ProyectoRepository : GenericRepo<Proyecto>, IProyecto
{
    private IProyectoIndicadore _ProyectoIndicadore;
    private IProyectoRiesgo _ProyectoRiesgo;
    private IAreasTransversale _AreaTransversales;
    private IActividade _Actividade;
    private IAuth Auth;
    public ProyectoRepository(PGIContext context, IProyectoIndicadore proyectoIndicadore, IAuth auth, IProyectoRiesgo proyectoRiesgo, IAreasTransversale areaTransversales, IActividade actividade) : base(context)
    {
        _ProyectoIndicadore = proyectoIndicadore;
        Auth = auth;
        _ProyectoRiesgo = proyectoRiesgo;
        _AreaTransversales = areaTransversales;
        _Actividade = actividade;
    }

    public override Proyecto AddOrUpdateSaving(Proyecto entity)
    {
        using (var trans = context.Database.BeginTransaction())
        {
            try
            {
                var data = base.AddOrUpdateSaving(entity);
                var proyectoIndicadores = _ProyectoIndicadore.FindAll(x => x.ProyectoId == data.Id);

                //Insertar indicadores
                if (entity.Indicadores.Count > 0)
                {
                    foreach (var item in entity.Indicadores)
                    {
                        if (proyectoIndicadores.Find(x => x.IndicadorId == item.Id) == null)
                        {
                            _ProyectoIndicadore.AddOrUpdateSaving(new ProyectoIndicadore()
                            {
                                CompaniaId = Auth.CurrentCompany.Id,
                                IndicadorId = item.Id,
                                ProyectoId = data.Id
                            });
                        }
                        else
                        {
                            var proyectIndicador = proyectoIndicadores.FirstOrDefault(x => x.IndicadorId == item.Id);
                            proyectoIndicadores.Remove(proyectIndicador);
                        }
                    }
                }
                else
                {
                    foreach (var item in proyectoIndicadores)
                    {
                        _ProyectoIndicadore.RemoveSaving(item);
                    }
                }
                //delete los no selecionados
                if (proyectoIndicadores.Count > 0 && entity.Indicadores.Count != 0)
                {
                    foreach (var item in proyectoIndicadores)
                    {
                        _ProyectoIndicadore.RemoveSaving(item);
                    }
                }
                //-------------------------------
                var proyectoRiesgos = _ProyectoRiesgo.FindAll(x => x.ProyectoId == data.Id);

                //Insertar Riesgos del proyecto----------------------------------
                if (entity.Riesgo.Count > 0)
                {
                    foreach (var item in entity.Riesgo)
                    {
                        if (proyectoRiesgos.Find(x => x.RiesgoId == item.Id) == null)
                        {
                            _ProyectoRiesgo.AddOrUpdateSaving(new ProyectoRiesgo()
                            {
                                CompaniaId = Auth.CurrentCompany.Id,
                                RiesgoId = item.Id,
                                ProyectoId = data.Id
                            });
                        }
                        else
                        {
                            var proyectoRiesgo = proyectoRiesgos.FirstOrDefault(x => x.RiesgoId == item.Id);
                            proyectoRiesgos.Remove(proyectoRiesgo);
                        }
                    }
                }
                else
                {
                    foreach (var item in proyectoRiesgos)
                    {
                        _ProyectoRiesgo.RemoveSaving(item);
                    }
                }
                //delete los no selecionados
                if (proyectoRiesgos.Count > 0 && entity.Riesgo.Count != 0)
                {
                    foreach (var item in proyectoRiesgos)
                    {
                        _ProyectoRiesgo.RemoveSaving(item);
                    }
                }
                //-------------------------------------
                //Insertar Riesgos del proyecto----------------------------------
                if (entity.Riesgo.Count > 0)
                {
                    foreach (var item in entity.Riesgo)
                    {
                        if (proyectoRiesgos.Find(x => x.RiesgoId == item.Id) == null)
                        {
                            _ProyectoRiesgo.AddOrUpdateSaving(new ProyectoRiesgo()
                            {
                                CompaniaId = Auth.CurrentCompany.Id,
                                RiesgoId = item.Id,
                                ProyectoId = data.Id
                            });
                        }
                        else
                        {
                            var proyectoRiesgo = proyectoRiesgos.FirstOrDefault(x => x.RiesgoId == item.Id);
                            proyectoRiesgos.Remove(proyectoRiesgo);
                        }
                    }
                }
                else
                {
                    foreach (var item in proyectoRiesgos)
                    {
                        _ProyectoRiesgo.RemoveSaving(item);
                    }
                }
                //delete los no selecionados
                if (proyectoRiesgos.Count > 0 && entity.Riesgo.Count != 0)
                {
                    foreach (var item in proyectoRiesgos)
                    {
                        _ProyectoRiesgo.RemoveSaving(item);
                    }
                }
                //-------------------------------------
                //Insertar Areas----------------------------------
                var areasTransversales = _AreaTransversales.FindAll(x => x.ProyectoId == data.Id);
                if (entity.Areas.Count > 0)
                {
                    foreach (var item in entity.Areas)
                    {
                        if (areasTransversales.Find(x => x.AreaId == item.Id) == null)
                        {
                            _AreaTransversales.AddOrUpdateSaving(new AreasTransversale()
                            {
                                CompaniaId = Auth.CurrentCompany.Id,
                                AreaId = item.Id,
                                ProyectoId = data.Id
                            });
                        }
                        else
                        {
                            var areastransversale = areasTransversales.FirstOrDefault(x => x.AreaId == item.Id);
                            areasTransversales.Remove(areastransversale);
                        }
                    }
                }
                else
                {
                    foreach (var item in areasTransversales)
                    {
                        _AreaTransversales.RemoveSaving(item);
                    }
                }
                //delete los no selecionados
                if (areasTransversales.Count > 0 && entity.Areas.Count != 0)
                {
                    foreach (var item in areasTransversales)
                    {
                        _AreaTransversales.RemoveSaving(item);
                    }
                }
                //-------------------------------------
                //Insertar Areas----------------------------------
                var actividades = _Actividade.FindAll(x => x.ProyectoId == data.Id);
                if (entity.actividades.Count > 0)
                {
                    foreach (var item in entity.actividades)
                    {
                        if (actividades.Find(x => x.Id == item.Id) == null)
                        {
                            _AreaTransversales.AddOrUpdateSaving(new AreasTransversale()
                            {
                                CompaniaId = Auth.CurrentCompany.Id,
                                AreaId = item.Id,
                                ProyectoId = data.Id
                            });
                        }
                        else
                        {
                            var actividad = actividades.FirstOrDefault(x => x.Id == item.Id);
                            if (actividad != null)
                            {
                                actividades.Remove(actividad);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in actividades)
                    {
                        _Actividade.RemoveSaving(item);
                    }
                }
                //delete los no selecionados
                if (actividades.Count > 0 && entity.Areas.Count != 0)
                {
                    foreach (var item in actividades)
                    {
                        _Actividade.RemoveSaving(item);
                    }
                }
                //-------------------------------------
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
