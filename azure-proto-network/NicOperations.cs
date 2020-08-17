using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{

    public class NicOperations : ArmResourceOperations<PhNetworkInterface, TagsObject, ArmOperation<PhNetworkInterface>, ArmOperation<Response>>
    {
        public NicOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NicOperations(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";


        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Context.ResourceGroup, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Context.ResourceGroup, Context.Name, cancellationToken));
        }

        public override Response<PhNetworkInterface> Get()
        {
            return new PhResponse<PhNetworkInterface, NetworkInterface>(Operations.Get(Context.ResourceGroup, Context.Name), n => new PhNetworkInterface(n));
        }

        public async override Task<Response<PhNetworkInterface>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhNetworkInterface, NetworkInterface>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, null, cancellationToken), n => new PhNetworkInterface(n));
        }

        public override ArmOperation<PhNetworkInterface> Update(TagsObject patchable)
        {
            return new PhArmOperation<PhNetworkInterface, NetworkInterface>(Operations.UpdateTags(Context.ResourceGroup, Context.Name, patchable), n => new PhNetworkInterface(n));
        }

        public async override Task<ArmOperation<PhNetworkInterface>> UpdateAsync(TagsObject patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhNetworkInterface, NetworkInterface>(await Operations.UpdateTagsAsync(Context.ResourceGroup, Context.Name, patchable, cancellationToken), n => new PhNetworkInterface(n));
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).NetworkInterfaces;

    }
}
