using azure_proto_core;
using System;

namespace azure_proto_authorization
{
    public class AuthorizationOperations : OperationsBase
    {
        public AuthorizationOperations(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public AuthorizationOperations(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public AuthorizationOperations(ArmClientContext context, Resource resource) : base(context, resource)
        {
        }

    }
}
