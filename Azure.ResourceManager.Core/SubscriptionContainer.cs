using Azure.Core;
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
        /// <param name="credential"> A credential used to authenticate to an Azure Service. </param>
        /// <param name="baseUri"> The base URI of the service. </param>
        internal SubscriptionContainer(AzureResourceManagerClientOptions options, TokenCredential credential, Uri baseUri)
            : base(options, null, credential, baseUri)
        {
        }

        /// <summary>
        /// Gets the valid resource type associated with the container.
        /// </summary>
        protected override ResourceType ValidResourceType => SubscriptionOperations.ResourceType;

        /// <summary>
        /// Gets the operations that can be performed on the container.
        /// </summary>
        private SubscriptionsOperations Operations => new ResourcesManagementClient(
            BaseUri,
            Guid.NewGuid().ToString(),
            Credential,
            ClientOptions.Convert<ResourcesManagementClientOptions>()).Subscriptions;

        /// <summary>
        /// Lists all subscriptions in the current container.
        /// </summary>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns> A collection of resource operations that may take multiple service requests to iterate over. </returns>
        public Pageable<Subscription> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<ResourceManager.Resources.Models.Subscription, Subscription>(
                Operations.List(cancellationToken),
                Converter());
        }

        /// <summary>
        /// Lists all subscriptions in the current container.
        /// </summary>
        /// <param name="cancellationToken">A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
        /// <returns> An async collection of resource operations that may take multiple service requests to iterate over. </returns>
        public AsyncPageable<Subscription> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<ResourceManager.Resources.Models.Subscription, Subscription>(
                Operations.ListAsync(cancellationToken),
                Converter());
        }

        /// <summary>
        /// Validate the resource identifier is supported in the current container.
        /// </summary>
        /// <param name="identifier"> The identifier of the resource. </param>
        protected override void Validate(ResourceIdentifier identifier)
        {
            if (identifier != null)
                throw new ArgumentException("Invalid parent for subscription container");
        }

        /// <summary>
        /// Gets the default subscription associated with the current credential.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns the subscription id. </returns>
        internal async Task<Subscription> GetDefaultSubscriptionAsync(CancellationToken cancellationToken = default)
        {
            var subs = ListAsync(cancellationToken).GetAsyncEnumerator();
            Subscription result = null;
            if (await subs.MoveNextAsync())
            {
                if (subs.Current != null)
                {
                    result = subs.Current;
                }
            }

            return result;
        }

        private Func<ResourceManager.Resources.Models.Subscription, Subscription> Converter()
        {
            return s => new Subscription(new SubscriptionOperations(ClientOptions, s.SubscriptionId, Credential, BaseUri), new SubscriptionData(s));
        }
    }
}
