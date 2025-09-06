using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


public interface IAuditoria : IGenericRepo<Auditoria>
{
}


public class AuditoriaRepository : GenericRepo<Auditoria>, IAuditoria
{

    public AuditoriaRepository(PGIContext context) : base(context)
    {
    }


}
