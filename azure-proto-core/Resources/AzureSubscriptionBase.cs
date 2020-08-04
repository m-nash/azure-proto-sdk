namespace azure_proto_core
{
    //TODO: If we decide to put base types plus core classes all in one this this class is no longer necessary
    public class AzureSubscriptionBase : AzureEntityHolder<TrackedResource>
    {
        public AzureSubscriptionBase(TrackedResource parent, TrackedResource model) : base(parent, model) { }
    }
}
