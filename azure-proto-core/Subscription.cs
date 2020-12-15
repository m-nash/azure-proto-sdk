namespace azure_proto_core
{
    public class Subscription : SubscriptionOperations
    {
        internal Subscription(ArmClientContext context, SubscriptionData resource, ArmClientOptions options)
            : base(context, resource, options)
        {
            Data = resource;
        }

        public SubscriptionData Data { get; private set; }
    }
}
