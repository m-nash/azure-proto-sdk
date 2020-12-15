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
        protected ResourceContainerOperations<TOperations, TResource> _unTypedContainerOperations;

        public ArmBuilder(ResourceContainerOperations<TOperations, TResource> containerOperations, TResource resource)
        {
            _resource = resource;
            _unTypedContainerOperations = containerOperations;
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

            return _unTypedContainerOperations.Create(name, _resource, cancellationToken);
        }

        public async Task<ArmResponse<TOperations>> CreateAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return await _unTypedContainerOperations.CreateAsync(name, _resource, cancellationToken);
        }

        public ArmOperation<TOperations> StartCreate(string name, CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return _unTypedContainerOperations.StartCreate(name, _resource, cancellationToken);
        }

        public async Task<ArmOperation<TOperations>> StartCreateAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            _resource = Build();

            return await _unTypedContainerOperations.StartCreateAsync(name, _resource, cancellationToken);
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
