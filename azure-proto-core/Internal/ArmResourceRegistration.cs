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
        Func<ArmClientContext, TrackedResource, ResourceContainerOperations<T>> _containerFactory;
        Func<ArmClientContext, ResourceIdentifier, ResourceCollectionOperations<T>> _collectionFactory;
        Func<ArmClientContext, Resource, ResourceOperationsBase<T>> _operationsFactory;


        public ArmResourceRegistration(
            ResourceType type, Func<ArmClientContext, TrackedResource, ResourceContainerOperations<T>> containerFactory,
            Func<ArmClientContext, ResourceIdentifier, ResourceCollectionOperations<T>> collectionFactory,
            Func<ArmClientContext, Resource, ResourceOperationsBase<T>> operationsFactory)
        {

            ResourceType = type;
            _containerFactory = containerFactory;
            _collectionFactory = collectionFactory;
            _operationsFactory = operationsFactory;

        }

        public Type ModelType => typeof(T);
        public ResourceType ResourceType { get; }

        public virtual bool HasOperations => _operationsFactory != null;
        public virtual bool HasContainer => _containerFactory != null;
        public virtual bool HasCollection => _collectionFactory != null;
        public ResourceOperationsBase<T> GetOperations(ArmClientContext parent, Resource context) => _operationsFactory(parent, context);
        public ResourceContainerOperations<T> GetContainer(ArmClientContext parent, TrackedResource parentContext) => _containerFactory(parent, parentContext);
        public ResourceCollectionOperations<T> GetCollection(ArmClientContext parent, ResourceIdentifier parentContext) => _collectionFactory(parent, parentContext);
    }
}
