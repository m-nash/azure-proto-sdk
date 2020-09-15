using System.Threading;
using System.Threading.Tasks;

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

        public virtual ArmResponse<ResourceOperationsBase<T>> Create(string name, CancellationToken cancellationToken = default)
        {
            return _unTypedContainerOperations.Create(name, _resource, cancellationToken);
        }

        public async virtual Task<ArmResponse<ResourceOperationsBase<T>>> CreateAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _unTypedContainerOperations.CreateAsync(name, _resource, cancellationToken);
        }

        public virtual ArmOperation<ResourceOperationsBase<T>> StartCreate(string name, CancellationToken cancellationToken = default)
        {
            return _unTypedContainerOperations.StartCreate(name, _resource, cancellationToken);
        }

        public async virtual Task<ArmOperation<ResourceOperationsBase<T>>> StartCreateAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _unTypedContainerOperations.StartCreateAsync(name, _resource, cancellationToken);
        }

        public virtual T Build()
        {
            return _resource;
        }
    }
}
