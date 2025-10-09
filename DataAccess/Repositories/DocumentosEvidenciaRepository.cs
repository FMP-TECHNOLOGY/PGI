using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IDocumentosEvidencia : IGenericRepo<DocumentosEvidencia>
{
    string delete(string id);

}


public class DocumentosEvidenciaRepository : GenericRepo<DocumentosEvidencia>, IDocumentosEvidencia
{

    public DocumentosEvidenciaRepository(PGIContext context) : base(context)
    {
    }

    public string delete(string id)
    {
        using (var db = context.Database.BeginTransaction())
        {
            try
            {

                var anexo = base.Find(x => x.Id == id);
                if (anexo == null) throw new Exception("Anexo no existe");


                if (anexo.Path != null)
                {
                    if (File.Exists(anexo.Path))
                    {
                        File.Delete(anexo.Path);
                    }
                }

                context.Documentosevidencias.Remove(anexo);
                context.SaveChanges();

                db.Commit();
                return "Elimidado";
            }
            catch (Exception ex)
            {
                db.Rollback();
                return ex.Message;
            }
        }
    }
}
