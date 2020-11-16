using Azure;
using Azure.ResourceManager.Authorization;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class RoleAssignmentOperations : ImmutableResourceOperationsBase<PhRoleAssignment, RoleAssignmentOperations>
    {
        public RoleAssignmentOperations(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public RoleAssignmentOperations(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public RoleAssignmentOperations(ArmClientContext context, Resource resource) : base(context, resource)
        {
        }

        public override ArmOperation<Response> Delete()
        {
            throw new NotImplementedException();
        }

        public override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override ArmResponse<RoleAssignmentOperations> Get()
        {
            throw new NotImplementedException();
        }

        public override Task<ArmResponse<RoleAssignmentOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        internal RoleAssignmentsOperations Operations => GetClient<AuthorizationManagementClient>((baseUri, creds) => new AuthorizationManagementClient( Id.Subscription, baseUri, creds)).RoleAssignments;

    }
}
