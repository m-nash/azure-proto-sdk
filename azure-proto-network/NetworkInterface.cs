﻿using System.Threading.Tasks;
using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    /// <summary>
    /// A class that represents a network interface in a resource group and the operatiosn that can be performed on it.
    /// </summary>
    public class NetworkInterface : NetworkInterfaceOperations
    {
        internal NetworkInterface(ResourceOperationsBase options, NetworkInterfaceData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the <see cref="NetworkInterfaceData"/> for this <see cref="NetworkInterface"/>.
        /// </summary>
        public NetworkInterfaceData Data { get; private set; }

        /// <inheritdoc />
        protected override NetworkInterface GetResource()
        {
            return this;
        }

        /// <inheritdoc />
        protected override Task<NetworkInterface> GetResourceAsync()
        {
            return Task.FromResult(this);
        }
    }
}
