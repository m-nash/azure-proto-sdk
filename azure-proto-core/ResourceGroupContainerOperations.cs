using Azure;
using Azure.Core;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Operations for the RespourceGroups container in the given subscription context.  Allows Creating and listign respource groups
    /// and provides an attachment point for Collections of Tracked Resources.
    /// </summary>
    public class ResourceGroupContainerOperations : ResourceContainerOperations<PhResourceGroup>
    {
        protected override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        internal ResourceGroupContainerOperations(ArmClientBase other, ResourceIdentifier context) : base(other, context)
        {
        }
        internal ResourceGroupContainerOperations(ArmClientBase other, Resource context) : base(other, context)
        {
        }

        public override ArmOperation<ResourceClientBase<PhResourceGroup>> Create(string name, PhResourceGroup resourceDetails)
        {
            return new PhArmOperation<ResourceClientBase<PhResourceGroup>, ResourceGroup>(Operations.CreateOrUpdate(name, resourceDetails), g => ResourceGroup(new PhResourceGroup(g)));
        }

        public ArmOperation<ResourceClientBase<PhResourceGroup>> Create(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));
            return new PhArmOperation<ResourceClientBase<PhResourceGroup>, ResourceGroup>(Operations.CreateOrUpdate(name, model), g => ResourceGroup(new PhResourceGroup(g)));
        }


        public async override Task<ArmOperation<ResourceClientBase<PhResourceGroup>>> CreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceClientBase<PhResourceGroup>, ResourceGroup>(await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken), g => ResourceGroup(new PhResourceGroup(g)));
        }

        public Pageable<ResourceClientBase<PhResourceGroup>> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<ResourceGroup, ResourceClientBase<PhResourceGroup>>(Operations.List(null, null, cancellationToken), s => ResourceGroup(new PhResourceGroup(s)));
        }

        public AsyncPageable<ResourceClientBase<PhResourceGroup>> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<ResourceGroup, ResourceClientBase<PhResourceGroup>>(Operations.ListAsync(null, null, cancellationToken), s => ResourceGroup(new PhResourceGroup(s)));
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Context.Subscription, cred)).ResourceGroups;

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier context)
        {
            return new ResourceGroupOperations(this, context);
        }

        public ResourceGroupOperations ResourceGroup(Resource context)
        {
            return new ResourceGroupOperations(this, context);
        }

        public ResourceGroupOperations ResourceGroup(string rg)
        {
            return new ResourceGroupOperations(this, $"{Context}/resourceGroups/{rg}");
        }



    }
}
