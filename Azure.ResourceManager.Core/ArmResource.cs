namespace Azure.ResourceManager.Core
{
    public class ArmResource : ArmResourceOperations
    {
        public ArmResource(ArmResourceOperations operations, ArmResourceData resource)
            : base(operations, resource.Id)
        {
            Data = resource;
        }

        public ArmResourceData Data { get; }

        private protected override ArmResource GetResource()
        {
            return this;
        }
    }
}
