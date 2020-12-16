using Azure.ResourceManager.Authorization;
using Azure.ResourceManager.Authorization.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_authorization
{
    /// <summary>
    /// Container for role assignments - note that in this case, the container is either a TrackedResource or a resource Id
    /// </summary>
    public class RoleAssignmentContainer : ExtensionResourceContainer<RoleAssignment, RoleAssignmentCreateParameters>
    {
        public RoleAssignmentContainer(OperationsBase operations) : base(operations)
        {
        }

        public RoleAssignmentContainer(ArmClientContext context, TrackedResource scope) : base(context, scope) { }
        public RoleAssignmentContainer(ArmClientContext context, ResourceIdentifier scope) : base(context, scope) { }

        public override Azure.ResourceManager.Core.ResourceType ResourceType => "Microsoft.Authorization/roleAssignments";

        //Discussion Point: Are there other instances of Asynchrony between creation and return scenarios?
        public override ArmResponse<RoleAssignment> Create(string name, RoleAssignmentCreateParameters resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.Create(Id, name, resourceDetails.ToModel(), cancellationToken);
            return new PhArmResponse<RoleAssignment, Azure.ResourceManager.Authorization.Models.RoleAssignment>(
                response,
                a => new RoleAssignment(ClientContext, new RoleAssignmentData(a)));
        }

        public async override Task<ArmResponse<RoleAssignment>> CreateAsync(string name, RoleAssignmentCreateParameters resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateAsync(Id, name, resourceDetails.ToModel(), cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<RoleAssignment, Azure.ResourceManager.Authorization.Models.RoleAssignment>(
                response,
                a => new RoleAssignment(ClientContext, new RoleAssignmentData(a)));
        }

        public override ArmOperation<RoleAssignment> StartCreate(string name, RoleAssignmentCreateParameters resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<RoleAssignment, Azure.ResourceManager.Authorization.Models.RoleAssignment>(
                Operations.Create(base.Id, name, resourceDetails.ToModel(), cancellationToken),
                a => new RoleAssignment(base.ClientContext, new RoleAssignmentData(a)));
        }

        public async override Task<ArmOperation<RoleAssignment>> StartCreateAsync(string name, RoleAssignmentCreateParameters resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<RoleAssignment, Azure.ResourceManager.Authorization.Models.RoleAssignment>(
                await Operations.CreateAsync(base.Id, name, resourceDetails.ToModel(), cancellationToken).ConfigureAwait(false),
                a => new RoleAssignment(base.ClientContext, new RoleAssignmentData(a)));
        }

        public override Azure.Pageable<RoleAssignment> ListAtScope(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public override Azure.AsyncPageable<RoleAssignment> ListAtScopeAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }


        internal RoleAssignmentsOperations Operations => GetClient<AuthorizationManagementClient>((baseUri, creds) => new AuthorizationManagementClient(Id.Subscription, baseUri, creds)).RoleAssignments;
    }
}
