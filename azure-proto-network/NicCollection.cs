﻿using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NicCollection : AzureCollection<AzureNic>
    {
        public NicCollection(TrackedResource resourceGroup) : base(resourceGroup) { }

        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(Parent.Id.Subscription);

        public AzureNic CreateOrUpdateNic(string name, AzureNic nic)
        {
            var nicResult = Client.NetworkInterfaces.StartCreateOrUpdate(Parent.Name, name, nic.Model).WaitForCompletionAsync().Result;
            nic = new AzureNic(Parent, new PhNetworkInterface(nicResult.Value));
            return nic;
        }

        public async Task<AzureNic> CreateOrUpdateNicAync(string name, AzureNic nic, CancellationToken cancellationToken = default)
        {
            var nicResult = await Client.NetworkInterfaces.StartCreateOrUpdateAsync(Parent.Name, name, nic.Model, cancellationToken);
            nic = new AzureNic(Parent, new PhNetworkInterface(nicResult.Value));
            return nic;
        }

        protected override IEnumerable<AzureNic> GetItems()
        {
            throw new NotImplementedException();
        }

        protected override AzureNic Get(string nicName)
        {
            var nicResult = Client.NetworkInterfaces.Get(Parent.Name, nicName);
            return new AzureNic(Parent, new PhNetworkInterface(nicResult.Value));
        }

        public AzureNic ConstructNic(AzurePublicIpAddress ip, string subnetId)
        {
            var nic = new NetworkInterface()
            {
                Location = Parent.Location,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = subnetId },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ip.Id }
                    }
                }
            };
            return new AzureNic(Parent, new PhNetworkInterface(nic));
        }
    }
}
