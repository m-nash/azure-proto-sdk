using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    /// <summary>
    /// Base class tracking context information for clients - when we have changed client constructors, this should not be necessary
    /// </summary>
    public abstract class ArmOperations
    {
        public ArmOperations(Uri baseUri, TokenCredential credential)
        {
            BaseUri = baseUri;
            Credential = credential;
        }

        public ArmOperations(ArmOperations other) : this(other.BaseUri, other.Credential)
        {
        }

        protected TokenCredential Credential { get; }

        protected Uri BaseUri { get; }

        protected abstract ResourceType ResourceType { get; }
        protected T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            return creator(BaseUri, Credential);
        }
    }
}
