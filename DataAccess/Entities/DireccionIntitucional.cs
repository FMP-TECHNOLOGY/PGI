using DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public partial class DireccionIntitucional : IUserIdentity, IIdentity, ICompanyIdentity
{
    public string? Id { get; set; }

    public string? Descripcion { get; set; }

    public string? Rnc { get; set; }

    public string? CompaniaId { get; set; }

    public string? UserId { get; set; }
    public string? Telefono { get; set; }

    //public int? ObjectType { get; }
    public bool? Active { get; set; }

    [NotMapped]
    public List<Sucursal> Sucursales { get; set; } = new();
}

class Circuito
{
    public int Id { get; set; }
    public int DocumentoId { get; set; }
    public string Nombre { get; set; }

    //etapas
    class Etapa
    {
        public int Id { get; set; }
        public int CircuitoId { get; set; }
        public string Nombre { get; set; }
        public string Accion { get; set; }

        public int MinAutorizaciones { get; set; }
        public int MinimoRechazos { get; set; }

        public int? EtapaRechazoId { get; set; }
        //public int? EtapaAprobacionId { get; set; }
        public int Orden { get; set; } = 1;

        //aprobadores
        class Aprobador
        {
            //public int Id { get; set; }
            public int EtapaId { get; set; }
            public int AprobadorId { get; set; }
            public string Tipo { get; set; }
        }
    }
}