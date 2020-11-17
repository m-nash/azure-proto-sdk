using Azure.ResourceManager.Authorization;
using Azure.ResourceManager.Authorization.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Container for role assignments - note that in this case, the container is either a TrackedResource or a resource Id
    /// </summary>
    public class RoleAssignmentContainer : ExtensionResourceContainer<RoleAssignmentOperations, PhRoleAssignment>
    {
        public RoleAssignmentContainer(ArmClientContext context, TrackedResource scope) : base(context, scope) { }
        public RoleAssignmentContainer(ArmClientContext context, ResourceIdentifier scope) : base(context, scope) { }

        public override azure_proto_core.ResourceType ResourceType => "Microsoft.Authorization/roleAssignments";
        public override ArmResponse<RoleAssignmentOperations> Create(string name, PhRoleAssignment resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.Create(Id, name, new RoleAssignmentCreateParameters(resourceDetails.RoleDefinitionId, resourceDetails.PrincipalId), cancellationToken);
            return new PhArmResponse<RoleAssignmentOperations, RoleAssignment>(
                response,
                a => new RoleAssignmentOperations(ClientContext, new PhRoleAssignment(a)));
        }

        public async override Task<ArmResponse<RoleAssignmentOperations>> CreateAsync(string name, PhRoleAssignment resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateAsync(Id, name, new RoleAssignmentCreateParameters(resourceDetails.RoleDefinitionId, 
                resourceDetails.PrincipalId), cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<RoleAssignmentOperations, RoleAssignment>(
                response,
                a => new RoleAssignmentOperations(ClientContext, new PhRoleAssignment(a)));
        }

        public override ArmOperation<RoleAssignmentOperations> StartCreate(string name, PhRoleAssignment resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<RoleAssignmentOperations, RoleAssignment>(
                Operations.Create(Id, name, new RoleAssignmentCreateParameters(resourceDetails.RoleDefinitionId,
                resourceDetails.PrincipalId), cancellationToken),
                a => new RoleAssignmentOperations(ClientContext, new PhRoleAssignment(a)));
        }

        public async override Task<ArmOperation<RoleAssignmentOperations>> StartCreateAsync(string name, PhRoleAssignment resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<RoleAssignmentOperations, RoleAssignment>(
                await Operations.CreateAsync(Id, name, new RoleAssignmentCreateParameters(resourceDetails.RoleDefinitionId,
                resourceDetails.PrincipalId), cancellationToken).ConfigureAwait(false),
                a => new RoleAssignmentOperations(ClientContext, new PhRoleAssignment(a)));
        }

        public override Azure.Pageable<PhRoleAssignment> ListAtScope(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public override Azure.AsyncPageable<PhRoleAssignment> ListAtScopeAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        internal RoleAssignmentsOperations Operations => GetClient<AuthorizationManagementClient>((baseUri, creds) => new AuthorizationManagementClient(Id.Subscription, baseUri, creds)).RoleAssignments;
    }
}
