using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    internal class LoginCredentials : ServiceClientCredentials
    {
        private string AuthenticationToken { get; set; }

        public override void InitializeServiceClient<T>(ServiceClient<T> client)
        {
            var clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");
            var tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");

            var authenticationContext = new AuthenticationContext(String.Format("https://login.windows.net/{0}", tenantId));

            var credential = new ClientCredential(clientId: clientId, clientSecret: clientSecret);

            var result = authenticationContext.AcquireTokenAsync(resource: "https://management.core.windows.net/",
                clientCredential: credential);

            if (result.Result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            AuthenticationToken = result.Result.AccessToken;
        }

        public override async Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (AuthenticationToken == null)
            {
                throw new InvalidOperationException("Token Provider Cannot Be Null");
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //request.Version = new Version(apiVersion);
            await base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
