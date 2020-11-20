using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_authorization
{
    public static class RoleAssignmentExtensions
    {
        public static RoleAssignmentContainer RoleAssignments(this ResourceOperationsBase resource)
        {
            return new RoleAssignmentContainer(resource);
        }

        public static RoleAssignmentContainer RoleAssignments(this SubscriptionOperations resource)
        {
            return new RoleAssignmentContainer(resource);
        }

        public static RoleAssignmentOperations RoleAssignment(this ResourceOperationsBase resource, string name)
        {
            return new RoleAssignmentOperations(resource.ClientContext, $"{resource.Id}/providers/Microsoft.Authorization/roleAssignments/{name}");
        }

        public static RoleAssignmentOperations RoleAssignment(this SubscriptionOperations resource, string name)
        {
            return new RoleAssignmentOperations(resource.ClientContext, $"{resource.Id}/providers/Microsoft.Authorization/roleAssignments/{name}");
        }

    }
}
