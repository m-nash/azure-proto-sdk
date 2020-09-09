using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    /// <summary>
    /// Base class tracking context information for clients - when we have changed client constructors, this should not be necessary
    /// </summary>
    public class ArmClientContext
    {
        public ArmClientContext(Uri baseUri, TokenCredential credential)
        {
            BaseUri = baseUri;
            Credential = credential;
        }

        public ArmClientContext(ArmClientContext other) : this(other.BaseUri, other.Credential)
        {
        }

        //public T GetOperations<T>(ResourceIdentifier context) where T : ResourceClientBase
        //{
        //    return Activator.CreateInstance(typeof(T), this, context) as T;
        //}

        //public T GetOperations<T>(azure_proto_core.Resource context) where T : ResourceClientBase
        //{
        //    return Activator.CreateInstance(typeof(T), this, context) as T;
        //}

        public virtual Location DefaultLocation { get; set; }

        protected TokenCredential Credential { get; }

        internal Uri BaseUri { get; }

        /// <summary>
        /// Note that this is currently adapting to underlying management clients - once generator changes are in, this would likely be unnecessary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creator"></param>
        /// <returns></returns>
        internal T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            return creator(BaseUri, Credential);
        }
    }
}
