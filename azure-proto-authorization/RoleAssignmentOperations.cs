using Azure;
using Azure.ResourceManager.Authorization;
using Azure.ResourceManager.Authorization.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_authorization
{
    public class RoleAssignmentOperations : ExtensionResourceOperationsBase<RoleAssignment>, IDeletable
    {
        public RoleAssignmentOperations(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public RoleAssignmentOperations(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResourceData(id)) { }

        public RoleAssignmentOperations(ArmClientContext context, Resource resource) : base(context, resource)
        {
        }

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.DeleteById(this.Id).GetRawResponse());
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
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
