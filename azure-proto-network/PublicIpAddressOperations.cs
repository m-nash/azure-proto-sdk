﻿using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpAddressOperations : ResourceOperationsBase<XPublicIpAddress, PhPublicIPAddress>, ITaggable<XPublicIpAddress, PhPublicIPAddress>, IDeletableResource<XPublicIpAddress, PhPublicIPAddress>
    {
        public PublicIpAddressOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync (Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<XPublicIpAddress> Get()
        {
            return new PhArmResponse<XPublicIpAddress, PublicIPAddress>(Operations.Get(Id.ResourceGroup, Id.Name), 
                n => { Resource = new PhPublicIPAddress(n); return new XPublicIpAddress(ClientContext, Resource as PhPublicIPAddress); });
        }

        public async override Task<ArmResponse<XPublicIpAddress>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<XPublicIpAddress, PublicIPAddress>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
               n => { Resource = new PhPublicIPAddress(n); return new XPublicIpAddress(ClientContext, Resource as PhPublicIPAddress); });
        }

        public ArmOperation<XPublicIpAddress> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<XPublicIpAddress, PublicIPAddress>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhPublicIPAddress(n); return new XPublicIpAddress(ClientContext, Resource as PhPublicIPAddress); });
        }

        public async Task<ArmOperation<XPublicIpAddress>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<XPublicIpAddress, PublicIPAddress>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhPublicIPAddress(n); return new XPublicIpAddress(ClientContext, Resource as PhPublicIPAddress); });
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).PublicIPAddresses;
    }
}
