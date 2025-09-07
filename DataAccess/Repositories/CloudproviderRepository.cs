using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface ICloudProvider : IGenericRepo<CloudProvider>
{
}


public class CloudProviderRepository : GenericRepo<CloudProvider>, ICloudProvider
{

    public CloudProviderRepository(PGIContext context) : base(context)
    {
    }


}
