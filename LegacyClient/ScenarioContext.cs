using System;
using Azure.Identity;
using Azure.Core;

namespace client
{
    class ScenarioContext
    {
        public static readonly string AzureSdkSandboxId = "db1ab6f0-4769-4b27-930e-01e2ef9c123c";

        public string VmName => $"{Environment.UserName}-vm";
        public string RgName { get; private set; }
        public string NsgName => $"{Environment.UserName}-test-nsg";
        public string SubscriptionId { get; private set; }
        public string Loc => "westus2";
        public string SubnetName => $"{Environment.UserName}-subnet";

        public TokenCredential Credential => new DefaultAzureCredential();

        public ScenarioContext() : this(AzureSdkSandboxId) { }

        public ScenarioContext(string subscriptionId)
        {
            RgName = $"{Environment.UserName}-{Environment.TickCount}-rg";
            SubscriptionId = subscriptionId;
        }
    }
}