using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IDocumentosEvidencia : IGenericRepo<DocumentosEvidencia>
{
}


public class DocumentosEvidenciaRepository : GenericRepo<DocumentosEvidencia>, IDocumentosEvidencia
{

    public DocumentosEvidenciaRepository(PGIContext context) : base(context)
    {
    }


}
