using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    /// <summary>
    /// A class representing a subnet along with the instance operations that can be performed on it.
    /// </summary>
    public class Subnet : SubnetOperations
    {
        /// <summary>
        /// A class representing a subnet along with the instance operations that can be performed on it.
        /// </summary>
        public Subnet(AzureResourceManagerClientOptions SubnetOptions, SubnetData resource)
            : base(SubnetOptions, resource.Id)
        {
            Data = resource;
        }

        public SubnetData Data { get; private set; }
    }
}
