using Azure;
using Azure.ResourceManager.Authorization;
using Azure.ResourceManager.Authorization.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_authorization
{
    public class RoleAssignmentOperations : ExtensionResourceOperationsBase<RoleAssignmentOperations, PhRoleAssignment>
    {
        public RoleAssignmentOperations(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public RoleAssignmentOperations(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public RoleAssignmentOperations(ArmClientContext context, Resource resource) : base(context, resource)
        {
        }

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.DeleteById(this.Id).GetRawResponse());
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation((await Operations.DeleteByIdAsync(this.Id, cancellationToken)).GetRawResponse());
        }

        public override ArmResponse<RoleAssignmentOperations> Get()
        {
            return new PhArmResponse<RoleAssignmentOperations, RoleAssignment>(Operations.GetById(this.Id), a => { Resource = new PhRoleAssignment(a); return this; });
        }

        public async override Task<ArmResponse<RoleAssignmentOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<RoleAssignmentOperations, RoleAssignment>(await Operations.GetByIdAsync(this.Id, cancellationToken), a => { Resource = new PhRoleAssignment(a); return this; });
        }

        internal RoleAssignmentsOperations Operations => GetClient<AuthorizationManagementClient>((baseUri, creds) => new AuthorizationManagementClient( Id.Subscription, baseUri, creds)).RoleAssignments;

    }
}
