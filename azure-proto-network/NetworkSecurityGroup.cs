// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;

namespace azure_proto_network
{
    /// <summary>
    /// A class representing the operations that can be performed over a specific network security group.
    /// </summary>
    public class NetworkSecurityGroup : NetworkSecurityGroupOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkSecurityGroup"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        public NetworkSecurityGroup(AzureResourceManagerClientOptions options, NetworkSecurityGroupData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the data representing this network security group.
        /// </summary>
        public NetworkSecurityGroupData Data { get; private set; }
    }
}
