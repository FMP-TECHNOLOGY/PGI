using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface IEvidencia : IGenericRepo<Evidencia>
{
}


public class EvidenciaRepository : GenericRepo<Evidencia>, IEvidencia
{

    public EvidenciaRepository(PGIContext context) : base(context)
    {
    }


}