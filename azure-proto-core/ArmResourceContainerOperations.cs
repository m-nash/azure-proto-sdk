using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: Refactor this as a set of interfaces at Container and Leaf scopes
    /// Create known Container and Leaf scopes for ARM Containers
    /// Think about how to extend known scope types in an extensible fashion (is it just adding them to the default, or is it having scopes for all provider or consumer services?
    /// For example, INetworkConsumer, IDatabaseConsumer, IEncryptionConsumer, IControlConsumer, ITriggerConsumer which also allows you to attach at that scope? [AttachDatabase]
    /// </summary>
    /// <typeparam name="T">The type of the resource model</typeparam>
    /// <typeparam name="U">The return type of the Creation methods, this can be Response<typeparamref name="T"/> or a long-running response</typeparam>
    public abstract class ArmResourceContainerOperations<T, U> : ArmResourceCollectionOperations where T : Resource where U : class
    {
        protected ArmResourceContainerOperations(ArmOperations parent, ResourceIdentifier contexts) : base(parent, contexts)
        {
        }
        protected ArmResourceContainerOperations(ArmOperations parent, Resource contexts) : base(parent, contexts)
        {
        }

        protected ArmResourceContainerOperations(ArmResourceOperations parent, ResourceIdentifier contexts) : base(parent, contexts)
        {
        }

        protected ArmResourceContainerOperations(ArmResourceOperations parent, Resource contexts) : base(parent, contexts)
        {
        }

        public virtual U Create(T resourceDetails)
        {
            return Create(resourceDetails.Id.Name, resourceDetails);
        }

        public abstract U Create(string name, T resourceDetails);
        public virtual Task<U> CreateAsync(T resourceDetails, CancellationToken cancellationToken = default)
        {
            return CreateAsync(resourceDetails?.Id?.Name, resourceDetails, cancellationToken);
        }

        public virtual IEnumerable<Location> ListAvailableLocations(CancellationToken cancellationToken = default)
        {
            return GetResourcesClient(Context.Subscription).Providers.List(expand: "metadata", cancellationToken: cancellationToken)
                .FirstOrDefault(p => string.Equals(p.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                .ResourceTypes.FirstOrDefault(r => ResourceType.Equals(r.ResourceType))
                .Locations.Select(l => new Location(l));
        }

        public async virtual IAsyncEnumerable<Location> ListAvailableLocationsAsync( [EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            await foreach (var provider in GetResourcesClient(Context.Subscription).Providers.ListAsync(expand: "metadata", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
            {
                if (string.Equals(provider.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                {
                    var foundResource = provider.ResourceTypes.FirstOrDefault(p => ResourceType.Equals(p.ResourceType));
                    foreach (var location in foundResource.Locations)
                    {
                        yield return new Location(location);
                    }
                }
            }
        }

        public abstract Task<U> CreateAsync(string name, T resourceDetails, CancellationToken cancellationToken = default);

    }
}
