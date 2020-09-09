using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class ArmBuilder<T>
        where T: Resource
    {
        private T _resource;
        private ResourceContainerOperations<T> _containerOperations;

        public ArmBuilder(ResourceContainerOperations<T> containerOperations, T resource)
        {
            _resource = resource;
            _containerOperations = containerOperations;
        }

        //TODO: should not allow both on the same type
        public virtual ArmOperation<ResourceClientBase<T>> Create(string name)
        {
            if (null == _resource)
            {
                throw new InvalidOperationException("You must pass in resource details.");
            }

            return _containerOperations.Create(name, _resource);
        }

        public virtual ArmOperation<ResourceClientBase<T>> Create()
        {
            if (null == _resource)
            {
                throw new InvalidOperationException("You must pass in resource details.");
            }

            return _containerOperations.Create(_resource);
        }
    }
}
