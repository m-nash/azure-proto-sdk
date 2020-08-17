using Azure;
using Azure.Core;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace azure_proto_core
{
    /// <summary>
    /// Generic list operations class - can be extended if a specific RP has more list operations
    /// </summary>
    public abstract class ArmResourceCollectionOperations : ArmResourceOperations
    {
        public ArmResourceCollectionOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public ArmResourceCollectionOperations(ArmOperations parent, Resource context) : this(parent, context?.Id)
        {
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier?.Type != ResourceType)
            {
                throw new InvalidOperationException($"{identifier} is not a valid resource of type '{ResourceType}'");
            }
        }

        public virtual Pageable<TrackedResource> List(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.SubstringFilter = filter;
            return ListAtContext(Context, innerFilter, top, cancellationToken);
        }

        public virtual AsyncPageable<TrackedResource> ListAsync(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.SubstringFilter = filter;
            return ListAtContextAsync(Context, innerFilter, top, cancellationToken);
        }

        public virtual Pageable<TrackedResource> ListByTag(ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.TagFilter = filter;
            return ListAtContext(Context, innerFilter, top, cancellationToken);
        }

        public virtual AsyncPageable<TrackedResource> ListByTagAsync(ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var innerFilter = new ArmFilterCollection(ResourceType);
            innerFilter.TagFilter = filter;
            return ListAtContextAsync(Context, innerFilter, top, cancellationToken);
        }


        protected Pageable<TrackedResource> ListAtContext(ResourceIdentifier context, ArmFilterCollection filters, int? top, CancellationToken cancellationToken = default)
        {
            Pageable<GenericResourceExpanded> result;
            if (context?.Type == "Microsoft.Resources/resourceGroups")
            {
                result = GetResourcesClient(context.Subscription)
                  .Resources.ListByResourceGroup(context.Name, filters.ToString(), null, top, cancellationToken);
            }
            else if (context?.Type == "Microsoft.Resources/subscriptions")
            {
                result = GetResourcesClient(context.Subscription)
                 .Resources.List(filters.ToString(), null, top, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Invalid context: {subscription}");
            }

            return new WrappingPageable<GenericResourceExpanded, TrackedResource>(result, s => new ArmResource(s.Id, s.Location));
        }

        protected AsyncPageable<TrackedResource> ListAtContextAsync(ResourceIdentifier context, ArmFilterCollection filters, int? top, CancellationToken cancellationToken = default)
        {
            AsyncPageable<GenericResourceExpanded> result;
            if (context?.Type == "Microsoft.Resources/resourceGroups")
            {
                result = GetResourcesClient(context.Subscription)
                  .Resources.ListByResourceGroupAsync(context.Name, filters.ToString(), null, top, cancellationToken);
            }
            else if (context?.Type == "Microsoft.Resources/subscriptions")
            {
                result = GetResourcesClient(context.Subscription)
                 .Resources.ListAsync(filters.ToString(), null, top, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Invalid context: {subscription}");
            }

            return new WrappingAsyncPageable<GenericResourceExpanded, TrackedResource>(result, s => new ArmResource(s.Id, s.Location));
        }

        protected ResourcesManagementClient GetResourcesClient(string subscription)
        {
            return GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, subscription, cred));
        }
    }
}
