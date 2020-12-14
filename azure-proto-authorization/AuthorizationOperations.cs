using azure_proto_core;
using System;

namespace azure_proto_authorization
{
    public class AuthorizationOperations : OperationsBase
    {
        public AuthorizationOperations(ArmResourceOperations genericOperations)
            : this(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        public AuthorizationOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions options = default)
            : base(context, id, options)
        {
        }

        public AuthorizationOperations(ArmClientContext context, Resource resource, ArmClientOptions options = default)
            : this(context, resource.Id)
        {
        }
    }
}
