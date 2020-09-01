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
        Func<ArmOperations, TrackedResource, ResourceContainerOperations<T>> _containerFactory;
        Func<ArmOperations, ResourceIdentifier, ResourceCollectionOperations<T>> _collectionFactory;
        Func<ArmOperations, Resource, ResourceOperations<T>> _operationsFactory;


        public ArmResourceRegistration(
            ResourceType type, Func<ArmOperations, TrackedResource, ResourceContainerOperations<T>> containerFactory,
            Func<ArmOperations, ResourceIdentifier, ResourceCollectionOperations<T>> collectionFactory,
            Func<ArmOperations, Resource, ResourceOperations<T>> operationsFactory)
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
        public ResourceOperations<T> GetOperations(ArmOperations parent, Resource context) => _operationsFactory(parent, context);
        public ResourceContainerOperations<T> GetContainer(ArmOperations parent, TrackedResource parentContext) => _containerFactory(parent, parentContext);
        public ResourceCollectionOperations<T> GetCollection(ArmOperations parent, ResourceIdentifier parentContext) => _collectionFactory(parent, parentContext);
    }
}
