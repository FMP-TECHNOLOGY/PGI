using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;

 
    public interface IXxactividade : IGenericRepo<Xxactividade>
    {
    }


public class XxactividadeRepository : GenericRepo<Xxactividade>, IXxactividade
{

    public XxactividadeRepository(PGIContext context) : base(context)
    {
    }


}