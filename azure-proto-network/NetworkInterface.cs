using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class NetworkInterface : NetworkInterfaceOperations
    {
        internal NetworkInterface(ResourceOperationsBase options, NetworkInterfaceData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        public NetworkInterfaceData Data { get; private set; }
    }
}
