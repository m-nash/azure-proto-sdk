using System;
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

        public ArmResponse<ResourceOperationsBase<T>> Create(string name, CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return _unTypedContainerOperations.Create(name, _resource, cancellationToken);
        }

        public async Task<ArmResponse<ResourceOperationsBase<T>>> CreateAsync(string name, CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return await _unTypedContainerOperations.CreateAsync(name, _resource, cancellationToken);
        }

        public ArmOperation<ResourceOperationsBase<T>> StartCreate(string name, CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return _unTypedContainerOperations.StartCreate(name, _resource, cancellationToken);
        }

        public async Task<ArmOperation<ResourceOperationsBase<T>>> StartCreateAsync(string name, CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return await _unTypedContainerOperations.StartCreateAsync(name, _resource, cancellationToken);
        }

        private void ThrowIfNotValid()
        {
            string message;
            if (!IsValid(out message))
                throw new InvalidOperationException(message);
        }

        protected virtual bool IsValid(out string message)
        {
            message = String.Empty;
            return true;
        }

        protected virtual void OnBeforeBuild()
        {
        }

        protected virtual void OnAfterBuild()
        {
        }

        private void _Build()
        {

        }

        public T Build()
        {
            ThrowIfNotValid();
            OnBeforeBuild();
            _Build();
            OnAfterBuild();
            return _resource;
        }
    }
}
