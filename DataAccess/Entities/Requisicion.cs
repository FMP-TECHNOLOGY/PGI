using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Requisicion
    {
        public string? Id { get; set; }

        public string? CompaniaId { get; set; }
        public int? ObjectType { get; }
        public string? DireccionInstitucionalId { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaRequerida { get; set; }
        public string? SucursalId { get; set; }
        public string? DepartamentoId { get; set; }
        public string? AreaId { get; set; }

        public string? UserId { get; set; }

        public DateTime Created { get; set; }

        public List<DetalleRequisicion> Detalles { get; set; } = new List<DetalleRequisicion>();

        //public string? ProgramaId { get; set; }
        //public string? FondoId { get; set; }

        //public string? PoaId { get; set; }

        //public string? AreaId { get; set; }

        //public string? Codigo { get; set; }

        //public string? Descripcion { get; set; }

        //public string? ObjetivoId { get; set; }
        //public List<Objetivo> Objetivos { get; set; } = new List<Objetivo>();
        //public string? Responsable { get; set; }

        //public string? UnidadMedidaId { get; set; }

        //public string? PeriodicidadId { get; set; }

        //public decimal LineaBase { get; set; }

        //public decimal Meta { get; set; }

        //public decimal Peso { get; set; }

        //public bool? Aprobado { get; set; }


        //producto
        //public string? Dimension1 { get; set; }
        ////actividad
        //public string? Dimension2 { get; set; }
        ////departamento gerencial
        //public string? Dimension3 { get; set; }
        ////objetal
        ////public string? Dimension4 { get; set; } 
        ////fondo
        //public string? Dimension5 { get; set; }

    }
}
