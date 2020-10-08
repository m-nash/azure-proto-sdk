using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core.Internal
{
    /// <summary>
    /// Lightweight dependency injection for List/create/operations methods.  Allows use of generic methods for
    /// constructing list, creation, and Patch/Post/Delete methods for a resource 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArmResourceRegistration<U, T>
        where T : TrackedResource
        where U : ResourceOperationsBase<T>
    {
        Func<ArmClientContext, TrackedResource, ResourceContainerOperations<U, T>> _containerFactory;
        Func<ArmClientContext, Resource, U> _operationsFactory;

        public ArmResourceRegistration(
            ResourceType type, Func<ArmClientContext, TrackedResource, ResourceContainerOperations<U, T>> containerFactory,
            Func<ArmClientContext, Resource, U> operationsFactory)
        {

            ResourceType = type;
            _containerFactory = containerFactory;
            _operationsFactory = operationsFactory;

        }

        public Type ModelType => typeof(T);
        public ResourceType ResourceType { get; }

        public virtual bool HasOperations => _operationsFactory != null;
        public virtual bool HasContainer => _containerFactory != null;
        public U GetOperations(ArmClientContext parent, Resource context) => _operationsFactory(parent, context);
        public ResourceContainerOperations<U, T> GetContainer(ArmClientContext parent, TrackedResource parentContext) => _containerFactory(parent, parentContext);
    }
}
