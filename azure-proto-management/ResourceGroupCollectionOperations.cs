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

namespace azure_proto_management
{
    public class ResourceGroupCollectionOperations : ArmResourceCollectionOperations
    {
        internal ResourceGroupCollectionOperations(Uri baseUri, TokenCredential credential, IEnumerable<ResourceIdentifier> contexts) : base(baseUri, credential, contexts)
        {
        }

        public Pageable<PhResourceGroup> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingPageable<ResourceGroup, PhResourceGroup>(Contexts.Select(subscription => GetResourcesClient(subscription.Subscription).ResourceGroups.List(null, null, cancellationToken)), s => new PhResourceGroup(s));
        }

        public Pageable<PhResourceGroup> ListResourceGroups(string subscriptionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingPageable<ResourceGroup, PhResourceGroup>(GetResourcesClient(subscriptionId).ResourceGroups.List(null, null, cancellationToken), s => new PhResourceGroup(s));
        }

        public Pageable<PhResourceGroup> ListResourceGroups(IEnumerable<string> subscriptionIds, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingPageable<ResourceGroup, PhResourceGroup>(subscriptionIds.Select(subscription => GetResourcesClient(subscription).ResourceGroups.List(null, null, cancellationToken)), s => new PhResourceGroup(s));
        }

        public AsyncPageable<PhResourceGroup> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingAsyncPageable<ResourceGroup, PhResourceGroup>(Contexts.Select(subscription => GetResourcesClient(subscription.Subscription).ResourceGroups.ListAsync(null, null, cancellationToken)), s => new PhResourceGroup(s));
        }

        public AsyncPageable<PhResourceGroup> ListResourceGroupsAsync(string subscriptionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingAsyncPageable<ResourceGroup, PhResourceGroup>(GetResourcesClient(subscriptionId).ResourceGroups.ListAsync(null, null, cancellationToken), s => new PhResourceGroup(s));
        }

        public AsyncPageable<PhResourceGroup> ListResourceGroupsAsync(IEnumerable<string> subscriptionIds, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WrappingAsyncPageable<ResourceGroup, PhResourceGroup>(subscriptionIds.Select(subscription => GetResourcesClient(subscription).ResourceGroups.ListAsync(null, null, cancellationToken)), s => new PhResourceGroup(s));
        }

        private ResourcesManagementClient GetResourcesClient(string subscription)
        {
            return GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, subscription, cred));
        }
    }
}
