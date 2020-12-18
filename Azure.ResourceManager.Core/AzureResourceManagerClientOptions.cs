using System;
using System.Collections.Generic;
using System.ComponentModel;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Identity;

namespace Azure.ResourceManager.Core
{
    public class AzureResourceManagerClientOptions : ClientOptions
    {
        private Dictionary<Type, object> _overrides = new Dictionary<Type, object>();

        private static readonly object _overridesLock = new object();

        public AzureResourceManagerClientOptions()
            : this(new Uri(AzureResourceManagerClient.DefaultUri), new DefaultAzureCredential())
        {
        }

        public AzureResourceManagerClientOptions(Uri baseUri, TokenCredential credential, AzureResourceManagerClientOptions other = null)
        {
            BaseUri = baseUri;
            Credential = credential;
            //Will go away when moved into core since we will have directy acces the policies and transport, so just need to set those
            if (!Object.ReferenceEquals(other, null))
                copy(other);
        }

        public AzureResourceManagerClientOptions(AzureResourceManagerClientOptions other)
            : this(other.BaseUri, other.Credential, other)
        {
        }

        public T Convert<T>()
            where T : ClientOptions, new()
        {
            var newOptions = new T();
            newOptions.Transport = this.Transport;
            foreach (var pol in PerCallPolicies)
            {
                newOptions.AddPolicy(pol, HttpPipelinePosition.PerCall);
            }

            foreach (var pol in PerRetryPolicies)
            {
                newOptions.AddPolicy(pol, HttpPipelinePosition.PerRetry);
            }

            return newOptions;
        }

        // Will be removed like AddPolicy when we move to azure core
        private void copy(AzureResourceManagerClientOptions other)
        {
            this.Transport = other.Transport;
            foreach (var pol in other.PerCallPolicies)
            {
                this.AddPolicy(pol, HttpPipelinePosition.PerCall);
            }

            foreach (var pol in other.PerRetryPolicies)
            {
                this.AddPolicy(pol, HttpPipelinePosition.PerRetry);
            }
        }
        // TODO policy lists are internal hence we don't have acces to them by inheriting ClientOptions in this Asembly, this is a wrapper for now to convert to the concrete
        // policy options.
        public new void AddPolicy(HttpPipelinePolicy policy, HttpPipelinePosition position)
        {
            switch (position)
            {
                case HttpPipelinePosition.PerCall:
                    PerCallPolicies.Add(policy);
                    break;
                case HttpPipelinePosition.PerRetry:
                    PerRetryPolicies.Add(policy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }

            base.AddPolicy(policy, position);
        }

        internal IList<HttpPipelinePolicy> PerCallPolicies { get; } = new List<HttpPipelinePolicy>();

        internal IList<HttpPipelinePolicy> PerRetryPolicies { get; } = new List<HttpPipelinePolicy>();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public object GetOverrideObject<T>(Func<object> ctor)
        {
            object overrideObject;
            Type type = typeof(T);
            if (!_overrides.TryGetValue(type, out overrideObject))
            {
                lock (_overridesLock)
                {
                    if (!_overrides.TryGetValue(type, out overrideObject))
                    {
                        overrideObject = ctor();
                        _overrides[type] = overrideObject;
                    }
                }
            }

            return overrideObject;
        }

        internal TokenCredential Credential { get; }


        internal Uri BaseUri { get; }


        /// <summary>
        ///     HTTP client options that will be used for all clients created from this Azure Resource Manger Client.
        /// </summary>

        /// <summary>
        ///     Note that this is currently adapting to underlying management clients - once generator changes are in, this would
        ///     likely be unnecessary
        /// </summary>
        /// <typeparam name="T">Operations class</typeparam>
        /// <param name="creator">Method to construct the operations class</param>
        /// <returns>Constructed operations class</returns>
        internal T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            return creator(BaseUri, Credential);
        }
    }
}