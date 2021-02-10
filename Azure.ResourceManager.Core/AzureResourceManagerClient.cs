// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// The entry point for all ARM clients.
    /// </summary>
    public sealed class AzureResourceManagerClient
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        internal static readonly string DefaultUri = "https://management.azure.com";

        private readonly TokenCredential _credentials;

        private readonly Uri _baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureResourceManagerClient"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        public AzureResourceManagerClient(AzureResourceManagerClientOptions options = default)
            : this(null, null, new DefaultAzureCredential(), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureResourceManagerClient"/> class.
        /// </summary>
        /// <param name="credential"> A credential used to authenticate to an Azure Service. </param>
        /// <param name="options"> The client parameters to use in these operations. </param>
        public AzureResourceManagerClient(TokenCredential credential, AzureResourceManagerClientOptions options = default)
            : this(null, null, credential, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureResourceManagerClient"/> class.
        /// </summary>
        /// <param name="defaultSubscriptionId"> The id of the default Azure subscription. </param>
        /// <param name="credential"> A credential used to authenticate to an Azure Service. </param>
        /// <param name="options"> The client parameters to use in these operations. </param>
        public AzureResourceManagerClient(string defaultSubscriptionId, TokenCredential credential, AzureResourceManagerClientOptions options = default)
            : this(defaultSubscriptionId, null, credential, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureResourceManagerClient"/> class.
        /// </summary>
        /// <param name="baseUri"> The base URI of the service. </param>
        /// <param name="credential"> A credential used to authenticate to an Azure Service. </param>
        /// <param name="options"> The client parameters to use in these operations. </param>
        public AzureResourceManagerClient(Uri baseUri, TokenCredential credential, AzureResourceManagerClientOptions options = default)
            : this(null, baseUri, credential, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureResourceManagerClient"/> class.
        /// </summary>
        /// <param name="defaultSubscriptionId"> The id of the default Azure subscription. </param>
        /// <param name="baseUri"> The base URI of the service. </param>
        /// <param name="credential"> A credential used to authenticate to an Azure Service. </param>
        /// <param name="options"> The client parameters to use in these operations. </param>
        private AzureResourceManagerClient(string defaultSubscriptionId, Uri baseUri, TokenCredential credential, AzureResourceManagerClientOptions options = default)
        {
            _credentials = credential;
            _baseUri = baseUri;
            ClientOptions = options ?? new AzureResourceManagerClientOptions();

            if (string.IsNullOrWhiteSpace(defaultSubscriptionId))
            {
                DefaultSubscription = GetDefaultSubscriptionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            else
            {
                DefaultSubscription = GetSubscriptionOperations(defaultSubscriptionId).Get().Value;
            }

            ApiVersionOverrides = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the Api version overrides.
        /// </summary>
        public Dictionary<string, string> ApiVersionOverrides { get; private set; }

        /// <summary>
        /// Gets the default Azure subscription.
        /// </summary>
        public Subscription DefaultSubscription { get; private set; }

        /// <summary>
        /// Gets the Azure resource manager client options.
        /// </summary>
        internal AzureResourceManagerClientOptions ClientOptions { get; }

        /// <summary>
        /// Gets the Azure subscription operations.
        /// </summary>
        /// <param name="subscriptionId"> The resource identifier of the subscription. </param>
        /// <returns> Subscription operations. </returns>
        public SubscriptionOperations GetSubscriptionOperations(ResourceIdentifier subscriptionId) => new SubscriptionOperations(
            ClientOptions,
            subscriptionId,
            _credentials,
            _baseUri);

        /// <summary>
        /// Gets the Azure subscription operations.
        /// </summary>
        /// <param name="subscriptionGuid"> The guid of the subscription. </param>
        /// <returns> Subscription operations. </returns>
        public SubscriptionOperations GetSubscriptionOperations(string subscriptionGuid) => new SubscriptionOperations(
            ClientOptions,
            subscriptionGuid,
            _credentials,
            _baseUri);

        /// <summary>
        /// Gets the Azure subscriptions.
        /// </summary>
        /// <returns> Subscription container. </returns>
        public SubscriptionContainer GetSubscriptionContainer()
        {
            return new SubscriptionContainer(ClientOptions, _credentials, _baseUri);
        }

        /// <summary>
        /// Gets resource group operations.
        /// </summary>
        /// <param name="subscriptionGuid"> The id of the Azure subscription. </param>
        /// <param name="resourceGroupName"> The resource group name. </param>
        /// <returns> Resource group operations. </returns>
        public ResourceGroupOperations GetResourceGroupOperations(string subscriptionGuid, string resourceGroupName)
        {
            return GetSubscriptionOperations(subscriptionGuid).GetResourceGroupOperations(resourceGroupName);
        }

        /// <summary>
        /// Gets resource group operations.
        /// </summary>
        /// <param name="resourceGroupId"> The resource identifier of the resource group. </param>
        /// <returns> Resource group operations. </returns>
        public ResourceGroupOperations GetResourceGroupOperations(ResourceIdentifier resourceGroupId)
        {
            return GetSubscriptionOperations(resourceGroupId.Subscription).GetResourceGroupOperations(resourceGroupId.ResourceGroup);
        }

        /// <summary>
        /// Gets resource operations base.
        /// </summary>
        /// <typeparam name="T"> The type of the underlying model this class wraps. </typeparam>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <param name="resourceGroup"> The resource group name. </param>
        /// <param name="name"> The resource type name. </param>
        /// <returns> Resource operations of the resource. </returns>
        public T GetResourceOperations<T>(string subscription, string resourceGroup, string name)
            where T : OperationsBase
        {
            var rgOp = GetSubscriptionOperations(subscription).GetResourceGroupOperations(resourceGroup);
            string resourceType = typeof(T).GetField("ResourceType", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null).ToString();
            return Activator.CreateInstance(
                typeof(T),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                null,
                new object[] { rgOp, name },
                CultureInfo.InvariantCulture) as T;
        }

        /// <summary>
        /// Gets resource operations base.
        /// </summary>
        /// <typeparam name="TContainer"> The type of the container class for a specific resource. </typeparam>
        /// <typeparam name="TOperations"> The type of the operations class for a specific resource. </typeparam>
        /// <typeparam name="TResource"> The type of the class containing properties for the underlying resource. </typeparam>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <param name="resourceGroup"> The resource group name. </param>
        /// <param name="name"> The resource type name. </param>
        /// <param name="model"> The resource data model. </param>
        /// <param name="location"> The resource geo-location. </param>
        /// <returns> Resource operations of the resource. </returns>
        public ArmResponse<TOperations> CreateResource<TContainer, TOperations, TResource>(string subscription, string resourceGroup, string name, TResource model, LocationData location = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations>
            where TContainer : ResourceContainerBase<TOperations, TResource>
        {
            if (location == null)
            {
                location = LocationData.Default;
            }

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientOptions, new GenericResourceData($"/subscriptions/{subscription}/resourceGroups/{resourceGroup}", location)) as TContainer;

            return container.Create(name, model);
        }

        /// <summary>
        /// Gets default subscription.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns the subscription id. </returns>
        internal async Task<Subscription> GetDefaultSubscriptionAsync(CancellationToken cancellationToken = default)
        {
            return await GetSubscriptionContainer().GetDefaultSubscriptionAsync(cancellationToken);
        }

        private ResourcesManagementClient GetResourcesClient(string subscription) => new ResourcesManagementClient(_baseUri, subscription, _credentials);
    }
}
