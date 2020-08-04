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
                scenario = ScenarioFactory.GetScenario(Scenarios.ShutdownVmsByTag);
                scenario.Execute();
            }
            finally
            {
                if (scenario != null)
                {
                    Console.WriteLine($"--------Deleting {scenario.Context.RgName}--------");
                    AzureResourceGroup rg = AzureClient.GetResourceGroup(scenario.Context.SubscriptionId, scenario.Context.RgName);
                    _ = rg.DeleteAsync();
                }
            }
        }
    }
}
