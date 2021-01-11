// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Core;

namespace azure_proto_authorization
{
    /// <summary>
    /// A Role Assignment for Role-based access control in ARM
    /// </summary>
    public class RoleAssignment : RoleAssignmentOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleAssignment"/> class.
        /// </summary>
        /// <param name="options">The http settings for the operations over this resource</param>
        /// <param name="data">The properties of the resource</param>
        public RoleAssignment(AzureResourceManagerClientOptions options, RoleAssignmentData data)
            : base(options, data?.Id)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the properties of the RoleAssignment.
        /// </summary>
        public RoleAssignmentData Data { get; }
    }
}
