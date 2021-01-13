using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// A class representing the operations that can be performed over a specific virtual nerwork.
    /// </summary>
    public class VirtualNetworkOperations : ResourceOperationsBase<VirtualNetwork>, ITaggableResource<VirtualNetwork>, IDeletableResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualNetworkOperations"/> class.
        /// </summary>
        /// <param name="genericOperations"> An instance of <see cref="ArmResourceOperations"/> that has an id for a virtual nerwork. </param>
        internal VirtualNetworkOperations(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualNetworkOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal VirtualNetworkOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Gets the resource type definition for a virtual nerwork.
        /// </summary>
        public static readonly ResourceType ResourceType = "Microsoft.Network/virtualNetworks";

        /// <summary>
        /// Gets the valid resource type definition for a virtual nerwork.
        /// </summary>
        protected override ResourceType ValidResourceType => ResourceType;

        /// <summary>
        /// Gets the client for a virtual nerwork.
        /// </summary>
        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            ClientOptions.Convert<NetworkManagementClientOptions>())).VirtualNetworks;

        /// <summary>
        /// The operation to delete a virtual nerwork. 
        /// </summary>
        /// <returns> A response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.StartDelete(Id.ResourceGroup, Id.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        /// <summary>
        /// The operation to delete a virtual nerwork. 
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmResponse((await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken)).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        /// <summary>
        /// The operation to delete a virtual nerwork.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> An <see cref="ArmOperation{Response}"/> that allows polling for completion of the operation. </returns>
        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        /// <summary>
        /// The operation to delete a virtual nerwork.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{Response}"/> that allows polling for completion of the operation. </returns>
        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        /// <inheritdoc/>
        public override ArmResponse<VirtualNetwork> Get()
        {
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.Get(Id.ResourceGroup, Id.Name),
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientOptions, Resource as VirtualNetworkData);
                });
        }

        /// <inheritdoc/>
        public async override Task<ArmResponse<VirtualNetwork>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientOptions, Resource as VirtualNetworkData);
                });
        }

        /// <summary>
        /// Adds a tag to a virtual network.
        /// If the tag already exists it will be modified.
        /// </summary>
        /// <param name="key"> The key for the tag. </param>
        /// <param name="value"> The value for the tag. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> An <see cref="ArmOperation{VirtualNetwork}"/> that allows polling for completion of the operation. </returns>
        public ArmOperation<VirtualNetwork> StartAddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientOptions, Resource as VirtualNetworkData);
                });
        }

        /// <summary>
        /// Adds a tag to a virtual network.
        /// If the tag already exists it will be modified.
        /// </summary>
        /// <param name="key"> The key for the tag. </param>
        /// <param name="value"> The value for the tag. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{VirtualNetwork}"/> that allows polling for completion of the operation. </returns>
        public async Task<ArmOperation<VirtualNetwork>> StartAddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientOptions, Resource as VirtualNetworkData);
                });
        }

        /// <summary>
        /// Gets a subnet in the virtual nerwork.
        /// </summary>
        /// <param name="subnet"> The data of the subnet. </param>
        /// <returns> An instance of Subnet. </returns>
        public Subnet Subnet(SubnetData subnet)
        {
            return new Subnet(ClientOptions, subnet);
        }

        /// <summary>
        /// Gets a subnet under in virtual nerwork.
        /// </summary>
        /// <param name="subnet"> The resource id of the subnet. </param>
        /// <returns> An instance of SubnetOperations. </returns>
        public SubnetOperations Subnet(ResourceIdentifier subnet)
        {
            return new SubnetOperations(ClientOptions, subnet);
        }

        /// <summary>
        /// Gets a subnet in the virtual nerwork.
        /// </summary>
        /// <param name="subnet"> The name of the subnet. </param>
        /// <returns> An instance of SubnetOperations. </returns>
        public SubnetOperations Subnet(string subnet)
        {
            return new SubnetOperations(ClientOptions, $"{Id}/subnets/{subnet}");
        }

        /// <summary>
        /// Gets a list of subnet in the virtual nerwork.
        /// </summary>
        /// <returns></returns>
        public virtual SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientOptions, Id);
        }
    }
}
