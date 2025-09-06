using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IDocumentosSolicitudCompra : IGenericRepo<DocumentosSolicitudCompra>
{
}


public class DocumentosSolicitudCompraRepository : GenericRepo<DocumentosSolicitudCompra>, IDocumentosSolicitudCompra
{

    public DocumentosSolicitudCompraRepository(PGIContext context) : base(context)
    {
    }


}
