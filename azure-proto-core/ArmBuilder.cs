namespace azure_proto_core
{
    public class ArmBuilder<T>
        where T: Resource
    {
        protected T _resource;
        protected ResourceContainerOperations<T> _unTypedContainerOperations;

        public ArmBuilder(ResourceContainerOperations<T> containerOperations, T resource)
        {
            _resource = resource;
            _unTypedContainerOperations = containerOperations;
        }

        public virtual ArmOperation<ResourceOperationsBase<T>> Create(string name)
        {
            return _unTypedContainerOperations.Create(name, _resource);
        }

        public virtual T Build()
        {
            return _resource;
        }
    }
}
