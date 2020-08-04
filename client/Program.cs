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
                scenario = ScenarioFactory.GetScenario(Scenarios.ShutdownVmsByNameAcrossSubscriptions);
                scenario.Execute();
            }
            finally
            {
                foreach (var rgName in scenario.CleanUp)
                {
                    Console.WriteLine($"--------Deleting {rgName}--------");
                    AzureResourceGroup rg = AzureClient.GetResourceGroup(scenario.Context.SubscriptionId, rgName);
                    _ = rg.DeleteAsync();
                }
            }
        }
    }
}
