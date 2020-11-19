using Azure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class ArmResourceOperations : GenericResourcesOperations<ArmResourceOperations, ArmResource>, ITagable<ArmResourceOperations, ArmResource>, IDeletableResource<ArmResourceOperations, ArmResource>
    {
        public ArmResourceOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override void Validate(ResourceIdentifier identifier)
        {
            //the id can be of any type so do nothing
        }

        //TODO: Fill out the methods using ResourceManagementClient 
        public  ArmOperation<ArmResourceOperations> AddTag(string key, string value)
        {
            throw new NotImplementedException();
        }

        public  Task<ArmOperation<ArmResourceOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public  ArmOperation<Response> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override ArmResponse<ArmResourceOperations> Get()
        {
            throw new NotImplementedException();
        }

        public override Task<ArmResponse<ArmResourceOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
