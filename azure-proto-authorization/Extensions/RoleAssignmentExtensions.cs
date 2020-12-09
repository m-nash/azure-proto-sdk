using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_authorization
{
    public static class RoleAssignmentExtensions
    {
        /// <summary>
        /// Get RoleAssignment Container for the given resource.  Note that this is only valid for unconstrained role assignments, so
        /// it is a generation-time decision if we include this.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static RoleAssignmentContainer RoleAssignments(this ResourceOperationsBase resource)
        {
            return new RoleAssignmentContainer(resource);
        }

        public static RoleAssignmentContainer RoleAssignments(this SubscriptionOperations resource)
        {
            return new RoleAssignmentContainer(resource);
        }

        public static RoleAssignmentContainer RoleAssigmentsAtScope(this SubscriptionOperations resource, ResourceIdentifier scope)
        {
            return new RoleAssignmentContainer(resource.ClientContext, scope);
        }
        public static RoleAssignmentContainer RoleAssigmentsAtScope(this SubscriptionOperations resource, Resource scope)
        {
            return new RoleAssignmentContainer(resource.ClientContext, scope.Id);
        }



        public static RoleAssignmentOperations RoleAssignment(this ResourceOperationsBase resource, string name)
        {
            return new RoleAssignmentOperations(resource.ClientContext, $"{resource.Id}/providers/Microsoft.Authorization/roleAssignments/{name}");
        }

        public static RoleAssignmentOperations RoleAssignment(this SubscriptionOperations resource, string name)
        {
            return new RoleAssignmentOperations(resource.ClientContext, $"{resource.Id}/providers/Microsoft.Authorization/roleAssignments/{name}");
        }

        public static RoleAssignmentOperations RoleAssignmentAtScope(this SubscriptionOperations resource, ResourceIdentifier resourceId)
        {
            return new RoleAssignmentOperations(resource.ClientContext, resourceId);
        }

        public static RoleAssignmentOperations RoleAssignmentAtScope(this SubscriptionOperations resource, RoleAssignmentData role)
        {
            return new RoleAssignmentOperations(resource.ClientContext, role);
        }

    }
}
