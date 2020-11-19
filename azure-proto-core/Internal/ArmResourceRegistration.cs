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
    public class ArmResourceRegistration<TContainer, TContainerParent, TOperations, TResource>
        where TResource : TrackedResource
        where TOperations : GenericResourcesOperations<TOperations, TResource>
        where TContainer : ResourceContainerOperations<TOperations, TResource>
    {
        Func<ArmClientContext, TContainerParent, TContainer> _containerFactory;
        Func<ArmClientContext, Resource, TOperations> _operationsFactory;

        public ArmResourceRegistration(
            ResourceType type, Func<ArmClientContext, TContainerParent, TContainer> containerFactory,
            Func<ArmClientContext, Resource, TOperations> operationsFactory)
        {

            ResourceType = type;
            _containerFactory = containerFactory;
            _operationsFactory = operationsFactory;

        }

        public Type ModelType => typeof(TResource);
        public ResourceType ResourceType { get; }

        public virtual bool HasOperations => _operationsFactory != null;
        public virtual bool HasContainer => _containerFactory != null;
        public TOperations GetOperations(ArmClientContext parent, Resource context) => _operationsFactory(parent, context);
        public TContainer GetContainer(ArmClientContext parent, TContainerParent parentContext) => _containerFactory(parent, parentContext);
    }
}
