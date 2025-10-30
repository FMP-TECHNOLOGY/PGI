using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ISystemData<T> : IGenericRepo<T> where T : BaseSystemData
    {
    }

    public abstract class SystemDataRepository<T> : GenericRepo<T>, ISystemData<T> where T : BaseSystemData
    {

        public SystemDataRepository(PGIContext context) : base(context)
        {
        }

    }
}
