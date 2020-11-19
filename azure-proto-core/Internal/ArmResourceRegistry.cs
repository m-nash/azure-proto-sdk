using azure_proto_core.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace azure_proto_core
{
    public class ArmResourceRegistry
    {
        internal IDictionary<Type, object> _registry = new ConcurrentDictionary<Type, object>();

        public void Register<TContainer, TContainerParent, TOperations, TResource>(ArmResourceRegistration<TContainer, TContainerParent, TOperations, TResource> registration)
            where TResource : TrackedResource
            where TOperations : GenericResourcesOperations<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {
            _registry[registration.ModelType] = registration;
        }

        /// <summary>
        /// Return the typed resource operations for a specific top-level provider resource
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="context"></param>
        /// <param name="operations"></param>
        /// <returns></returns>
        public bool TryGetOperations<TContainer, TContainerParent, TOperations, TResource>(ArmClientContext parent, TrackedResource context, out TOperations operations)
            where TResource : TrackedResource
            where TOperations : GenericResourcesOperations<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {
            operations = null;
            ArmResourceRegistration<TContainer, TContainerParent, TOperations, TResource> registration;
            if (!TryGetRegistration<TContainer, TContainerParent, TOperations, TResource>(out registration) || !registration.HasOperations)
            {
                return false;
            }

            operations = registration.GetOperations(parent, context);
            return true;
        }

        public bool TryGetContainer<TContainer, TContainerParent, TOperations, TResource>(ArmClientContext parent, TContainerParent parentContext, out TContainer container)
            where TResource : TrackedResource
            where TOperations : GenericResourcesOperations<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {
            container = null;
            ArmResourceRegistration<TContainer, TContainerParent, TOperations, TResource> registration;
            if (!TryGetRegistration<TContainer, TContainerParent, TOperations, TResource>(out registration) || !registration.HasContainer)
            {
                return false;
            }

            container = registration.GetContainer(parent, parentContext);
            return true;
        }

        public bool TryGetResourceType<TContainer, TContainerParent, TOperations, TResource>(out ResourceType type)
            where TResource : TrackedResource
            where TOperations : GenericResourcesOperations<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {
            type = ResourceType.None;
            ArmResourceRegistration<TContainer, TContainerParent, TOperations, TResource> registration;
            if (!TryGetRegistration<TContainer, TContainerParent, TOperations, TResource>(out registration))
            {
                return false;
            }

            type = registration.ResourceType;
            return true;
        }

        internal bool TryGetRegistration<TContainer, TContainerParent, TOperations, TResource>(out ArmResourceRegistration<TContainer, TContainerParent, TOperations, TResource> registration)
            where TResource : TrackedResource
            where TOperations : GenericResourcesOperations<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
        {
            registration = null;
            object regObject;
            if (!_registry.TryGetValue(typeof(TResource), out regObject) || ((registration = regObject as ArmResourceRegistration<TContainer, TContainerParent, TOperations, TResource>) == null))
            {
                return false;
            }

            return true;
        }
    }
}
