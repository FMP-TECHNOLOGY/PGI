using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

    public interface IXactividade : IGenericRepo<Xactividade>
{
}


public class XactividadeRepository : GenericRepo<Xactividade>, IXactividade
{

    public XactividadeRepository(PGIContext context) : base(context)
    {
    }


}