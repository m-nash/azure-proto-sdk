using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    /// <summary>
    /// Base class tracking context information for clients - when we have changed client constructors, this should not be necessary
    /// </summary>
    public abstract class ArmClientBase
    {
        public ArmClientBase(Uri baseUri, TokenCredential credential)
        {
            BaseUri = baseUri;
            Credential = credential;
        }

        public ArmClientBase(ArmClientBase other) : this(other.BaseUri, other.Credential)
        {
        }

        //public T GetOperations<T>(ResourceIdentifier context) where T : ResourceOperations
        //{
        //    return Activator.CreateInstance(typeof(T), this, context) as T;
        //}

        //public T GetOperations<T>(azure_proto_core.Resource context) where T : ResourceOperations
        //{
        //    return Activator.CreateInstance(typeof(T), this, context) as T;
        //}

        public virtual Location DefaultLocation { get; set; }
        protected TokenCredential Credential { get; }

        protected Uri BaseUri { get; }

        protected abstract ResourceType ResourceType { get; }
        protected T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            return creator(BaseUri, Credential);
        }
    }
}
