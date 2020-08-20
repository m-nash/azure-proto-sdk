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

        internal ResourceGroupContainerOperations(ArmOperations other, ResourceIdentifier context) : base(other, context)
        {
        }
        internal ResourceGroupContainerOperations(ArmOperations other, Resource context) : base(other, context)
        {
        }

        public override ArmOperation<PhResourceGroup> Create(string name, PhResourceGroup resourceDetails)
        {
            return new PhArmOperation<PhResourceGroup, ResourceGroup>(GetRgOperations(Context.Subscription).CreateOrUpdate(name, resourceDetails), g => new PhResourceGroup(g));
        }

        public ArmOperation<PhResourceGroup> Create(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));
            return new PhArmOperation<PhResourceGroup, ResourceGroup>(GetRgOperations(Context.Subscription).CreateOrUpdate(name, model), g => new PhResourceGroup(g));
        }


        public async override Task<ArmOperation<PhResourceGroup>> CreateAsync(string name, PhResourceGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhResourceGroup, ResourceGroup>(await GetRgOperations(Context.Subscription).CreateOrUpdateAsync(name, resourceDetails, cancellationToken), g => new PhResourceGroup(g));
        }

        public Pageable<PhResourceGroup> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingPageable<ResourceGroup, PhResourceGroup>(GetRgOperations(Context.Subscription).List(null, null, cancellationToken), s => new PhResourceGroup(s));
        }

        public AsyncPageable<PhResourceGroup> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingAsyncPageable<ResourceGroup, PhResourceGroup>(GetRgOperations(Context.Subscription).ListAsync(null, null, cancellationToken), s => new PhResourceGroup(s));
        }


        internal ResourceGroupsOperations GetRgOperations(string subscriptionId) => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, subscriptionId, cred)).ResourceGroups;

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
