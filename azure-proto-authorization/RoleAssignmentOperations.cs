using Azure;
using Azure.ResourceManager.Authorization;
using Azure.ResourceManager.Authorization.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_authorization
{
    public class RoleAssignmentOperations : ExtensionResourceOperationsBase<RoleAssignment>, IDeletableResource
    {
        public RoleAssignmentOperations(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public RoleAssignmentOperations(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResourceData(id)) { }

        public RoleAssignmentOperations(ArmClientContext context, Resource resource) : base(context, resource)
        {
        }

        public ArmResponse<Response> Delete()
        {
            return new ArmVoidResponse(Operations.DeleteById(Id).GetRawResponse());
        }

        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidResponse((await Operations.DeleteByIdAsync(Id)).GetRawResponse());
        }


        public ArmOperation<Response> StartDelete()
        {
            return new ArmVoidOperation(Operations.DeleteById(Id).GetRawResponse());
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation((await Operations.DeleteByIdAsync(this.Id, cancellationToken)).GetRawResponse());
        }

        public override ArmResponse<RoleAssignment> Get()
        {
            return new PhArmResponse<RoleAssignment, Azure.ResourceManager.Authorization.Models.RoleAssignment>(Operations.GetById(this.Id), a => new RoleAssignment(this.ClientContext, new RoleAssignmentData(a)));
        }

        public async override Task<ArmResponse<RoleAssignment>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<RoleAssignment, Azure.ResourceManager.Authorization.Models.RoleAssignment>(await Operations.GetByIdAsync(this.Id, cancellationToken), a => new RoleAssignment(this.ClientContext, new RoleAssignmentData(a)));
        }

        internal RoleAssignmentsOperations Operations => GetClient<AuthorizationManagementClient>((baseUri, creds) => new AuthorizationManagementClient( Id.Subscription, baseUri, creds)).RoleAssignments;

    }
}
