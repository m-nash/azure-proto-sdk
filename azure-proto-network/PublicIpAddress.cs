using azure_proto_core;

namespace azure_proto_network
{
    public class PublicIpAddress : PublicIpAddressOperations
    {
        internal PublicIpAddress(ArmClientContext context, PublicIPAddressData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public PublicIPAddressData Data { get; private set; }
    }
}
