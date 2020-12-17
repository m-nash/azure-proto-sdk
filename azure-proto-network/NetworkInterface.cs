using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class NetworkInterface : NetworkInterfaceOperations
    {
        internal NetworkInterface(AzureResourceManagerClientContext context, NetworkInterfaceData resource)
            : base(context, resource.Id)
        {
            Data = resource;
        }

        public NetworkInterfaceData Data { get; private set; }
    }
}
