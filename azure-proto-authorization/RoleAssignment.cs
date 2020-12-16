using Azure.ResourceManager.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_authorization
{
    public class RoleAssignment : RoleAssignmentOperations
    {
        public RoleAssignment(ArmClientContext context, RoleAssignmentData data)
            : base(context, data?.Id)
        {
            Data = data;
        }

        public RoleAssignmentData Data { get; }
    }
}
