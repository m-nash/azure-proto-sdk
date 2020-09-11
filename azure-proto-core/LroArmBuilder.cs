using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    //TODO: Need to address the naming of this object
    public abstract class LroArmBuilder<U, T> : ArmBuilder< T>
        where U : ResourceContainerOperations<T>
        where T : Resource
    {
        protected U _containerOperations => _unTypedContainerOperations as U;

        public LroArmBuilder(U containerOperations, T resource) : base(containerOperations, resource) { }

        //TODO: should not allow both create(string name) and create() on the same type
        public abstract ArmOperation<ResourceOperationsBase<T>> StartCreate(string name);
    }
}
