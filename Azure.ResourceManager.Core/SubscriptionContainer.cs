using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing collection of Subscription and their operations
    /// </summary>
    public class SubscriptionContainer : OperationsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionContainer"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        internal SubscriptionContainer(AzureResourceManagerClientOptions options)
            : base(options, ResourceIdentifier.Undefined)
        {
        }

        /// <summary>
        /// Gets the operations that can be performed on the container.
        /// </summary>
        internal SubscriptionsOperations Operations => GetClient((uri, cred) =>
            new ResourcesManagementClient(
                uri,
                Guid.NewGuid().ToString(),
                cred,
                ClientOptions.Convert<ResourcesManagementClientOptions>())).Subscriptions;

        /// <summary>
        /// Gets the valid resource type associated with the container.
        /// </summary>
        /// <returns> A valid Azure resource type. </returns>
        protected override ResourceType ValidResourceType => new ResourceIdentifier(ResourceIdentifier.Undefined).Type; //.resource type at end 

        /// <summary>
        /// Lists all subscriptions in the current container.
        /// </summary>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        public Pageable<SubscriptionOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<ResourceManager.Resources.Models.Subscription, SubscriptionOperations>(
                Operations.List(cancellationToken),
                Converter());
        }

        /// <summary>
        /// Lists all subscriptions in the current container.
        /// </summary>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns> An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<SubscriptionOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<ResourceManager.Resources.Models.Subscription, SubscriptionOperations>(
                Operations.ListAsync(cancellationToken),
                Converter());
        }

        /// <summary>
        /// Validate the resource identifier is supported in the current container.
        /// </summary>
        /// <param name="identifier"> The identifier of the resource. </param>
        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier.Type != ResourceIdentifier.Undefined)
                throw new ArgumentException("Subscription container's parent must be none");
        }

        /// <summary>
        /// Gets the default subscription associated with the current credential.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns the subscription id. </returns>
        internal async Task<string> GetDefaultSubscriptionAsync(CancellationToken cancellationToken = default)
        {
            var subs = ListAsync(cancellationToken).GetAsyncEnumerator();
            string sub = null;
            if (await subs.MoveNextAsync())
            {
                if (subs.Current != null)
                {
                    sub = subs.Current.Id.Subscription;
                }
            }

            return sub;
        }

        private Func<ResourceManager.Resources.Models.Subscription, SubscriptionOperations> Converter()
        {
            return s => new SubscriptionOperations(ClientOptions, new SubscriptionData(s));
        }
    }
}
