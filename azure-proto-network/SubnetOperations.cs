using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// </summary>
    public class SubnetOperations : ResourceOperationsBase<Subnet>, IDeletableResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubnetOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal SubnetOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }
        /// <summary>
        /// Gets the resource type definition for a subnet.
        /// </summary>
        public static readonly ResourceType ResourceType = "Microsoft.Network/virtualNetworks/subnets";

        /// <summary>
        /// Gets the valid resource type definition for a subnet.
        /// </summary>
        protected override ResourceType ValidResourceType => ResourceType;

        private SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ClientOptions.Convert<NetworkManagementClientOptions>())).Subnets;

        /// <summary>
        /// The operation to delete a subnet. 
        /// </summary>
        /// <returns> A response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.StartDelete(Id.ResourceGroup, Id.Parent.Name, Id.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        /// <summary>
        /// The operation to delete a subnet. 
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmResponse((await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name)).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }


        /// <summary>
        /// The operation to delete a subnet.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> An <see cref="ArmOperation{Response}"/> that allows polling for completion of the operation. </returns>
        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Parent.Name, Id.Name));
        }

        /// <summary>
        /// The operation to delete a subnet.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{Response}"/> that allows polling for completion of the operation. </returns>
        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, cancellationToken));
        }

        /// <inheritdoc/>
        public override ArmResponse<Subnet> Get()
        {
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(Operations.Get(Id.ResourceGroup, Id.Parent.Name, Id.Name),
                n =>
                {
                    Resource = new SubnetData(n, DefaultLocation);
                    return new Subnet(ClientOptions, Resource as SubnetData);
                });
        }
        
        /// <inheritdoc/>
        public async override Task<ArmResponse<Subnet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<Subnet, Azure.ResourceManager.Network.Models.Subnet>(await Operations.GetAsync(Id.ResourceGroup, Id.Parent.Name, Id.Name, null, cancellationToken),
                n =>
                {
                    Resource = new SubnetData(n, DefaultLocation);
                    return new Subnet(ClientOptions, Resource as SubnetData);
                });
        }
    }
}
