// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    public class ArmBuilder<TOperations, TResource>
        where TResource : Resource
        where TOperations : ResourceOperationsBase<TOperations, TResource>
    {
        protected TResource _resource;
        protected ResourceContainerBase<TOperations, TResource> _unTypedContainer;

        public ArmBuilder(ResourceContainerBase<TOperations, TResource> container, TResource resource)
        {
            _resource = resource;
            _unTypedContainer = container;
        }

        public TResource Build()
        {
            ThrowIfNotValid();
            OnBeforeBuild();
            _Build();
            OnAfterBuild();

            return _resource;
        }

        public ArmResponse<TOperations> Create(string name, CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return _unTypedContainer.Create(name, _resource, cancellationToken);
        }

        public async Task<ArmResponse<TOperations>> CreateAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return await _unTypedContainer.CreateAsync(name, _resource, cancellationToken);
        }

        public ArmOperation<TOperations> StartCreate(string name, CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return _unTypedContainer.StartCreate(name, _resource, cancellationToken);
        }

        public async Task<ArmOperation<TOperations>> StartCreateAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return await _unTypedContainer.StartCreateAsync(name, _resource, cancellationToken);
        }

        protected virtual bool IsValid(out string message)
        {
            message = string.Empty;

            return true;
        }

        protected virtual void OnAfterBuild()
        {
        }

        protected virtual void OnBeforeBuild()
        {
        }

        private void _Build()
        {
        }

        private void ThrowIfNotValid()
        {
            string message;

            if (!IsValid(out message))
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
