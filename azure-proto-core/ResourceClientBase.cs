using Azure;
using Azure.ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Common base type for lifecycle operations over a resource
    /// TODO: Consider whehter to represent POST operations and the acommpanyong actions list call
    /// TODO: Consider whether to provide a Normalized PATCH functionality across RP resources
    /// TODO: Refactor methods beyond the ResourceOperation as extensions [allowing them to appear in generic usage of the type]
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class ResourceClientBase<T> : ResourceOperationsBase where T : Resource 
    {
        public ResourceClientBase(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
            Resource = new ArmResource(context);
        }

        public ResourceClientBase(ArmClientBase parent, Resource context) : base(parent, context)
        {
            Resource = context;
        }

        protected override Resource Resource { get;  set; }

        public override ResourceIdentifier Context => Resource.Id;

        public virtual IEnumerable<Location> ListAvailableLocations(ResourceType resourceType, CancellationToken cancellationToken = default)
        {
            return Providers.List(expand: "metadata", cancellationToken: cancellationToken)
                .FirstOrDefault(p => string.Equals(p.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                .ResourceTypes.FirstOrDefault(r => ResourceType.Equals(r.ResourceType))
                .Locations.Select(l => new Location(l));
        }

        public async virtual IAsyncEnumerable<Location> ListAvailableLocationsAsync(ResourceType resourceType, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var provider in Providers.ListAsync(expand: "metadata", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
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

        protected ProvidersOperations Providers => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Context.Subscription, cred)).Providers;

        public virtual bool HasModel { 
            get 
            {
                var model = Resource as T;
                return model != null;
            }
        }

        public virtual T Model
        {
            get
            {
                return Resource as T;
            }
        }

        public async virtual Task<T> GetModelIfNewerAsync(CancellationToken cancellationToken = default)
        {
            if (HasModel)
            {
                return Model;
            }

            return (await GetAsync(cancellationToken)).Value.Model;
        }

        public virtual T GetModelIfNewer()
        {
            if (HasModel)
            {
                return Model;
            }

            return Get().Value.Model;
        }

        public abstract Response<ResourceClientBase<T>> Get();
        public abstract Task<Response<ResourceClientBase<T>>> GetAsync(CancellationToken cancellationToken = default);
        public abstract ArmOperation<ResourceClientBase<T>> AddTag(string key, string value);
        public abstract Task<ArmOperation<ResourceClientBase<T>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }
}
