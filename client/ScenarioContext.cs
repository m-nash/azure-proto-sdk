using System;

namespace client
{
    class ScenarioContext
    {
        public string VmName => $"{Environment.UserName}-quickstartvm";
        public string RgName { get; private set; }
        public string NsgName => $"{Environment.UserName}-test-nsg";
        public string SubscriptionId => Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        public string Loc => "westus2";
        public string SubnetName => $"{Environment.UserName}-subnet";

        public ScenarioContext()
        {
            RgName = $"{Environment.UserName}-{Environment.TickCount}-rg";
        }
    }
}
