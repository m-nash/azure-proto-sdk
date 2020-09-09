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
    public class ArmResourceRegistration<T> where T : TrackedResource
    {
        Func<ArmClientBase, TrackedResource, ResourceContainerOperations<T>> _containerFactory;
        Func<ArmClientBase, Resource, ResourceClientBase<T>> _operationsFactory;


        public ArmResourceRegistration(
            ResourceType type, Func<ArmClientBase, TrackedResource, ResourceContainerOperations<T>> containerFactory,
            Func<ArmClientBase, Resource, ResourceClientBase<T>> operationsFactory)
        {

            ResourceType = type;
            _containerFactory = containerFactory;
            _operationsFactory = operationsFactory;

        }

        public Type ModelType => typeof(T);
        public ResourceType ResourceType { get; }

        public virtual bool HasOperations => _operationsFactory != null;
        public virtual bool HasContainer => _containerFactory != null;
        public ResourceClientBase<T> GetOperations(ArmClientBase parent, Resource context) => _operationsFactory(parent, context);
        public ResourceContainerOperations<T> GetContainer(ArmClientBase parent, TrackedResource parentContext) => _containerFactory(parent, parentContext);
    }
}
