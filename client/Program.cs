using azure_proto_core;
using azure_proto_management;
using System;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            Scenario scenario = null;
            try
            {
                scenario = ScenarioFactory.GetScenario(Scenarios.CreateSingleVmExample);
                scenario.Execute();
            }
            finally
            {
                foreach (var rgId in Scenario.CleanUp)
                {
                    ResourceIdentifier id = new ResourceIdentifier(rgId);
                    var sub = AzureClient.GetSubscription(id.Subscription);
                    AzureResourceGroup rg;
                    if (sub.ResourceGroups.TryGetValue(id.ResourceGroup, out rg))
                    {
                        Console.WriteLine($"--------Deleting {rg.Name}--------");
                        _ = rg.DeleteAsync();
                    }
                }
            }
        }
    }
}
