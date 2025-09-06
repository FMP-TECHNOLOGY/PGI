using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories;


    public interface ICloudprovider : IGenericRepo<Cloudprovider>
{
}


public class CloudproviderRepository : GenericRepo<Cloudprovider>, ICloudprovider
{

    public CloudproviderRepository(PGIContext context) : base(context)
    {
    }


}
