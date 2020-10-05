using azure_proto_core;
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
                foreach (var rgId in Scenario.CleanUp)
                {
                    ResourceIdentifier id = new ResourceIdentifier(rgId);
                    var rg = new ArmClient().Subscription(id.Subscription).ResourceGroup(id);
                    Console.WriteLine($"--------Deleting {rg.Id.Name}--------");
                    var rgModel = rg.GetModelIfNewer();
                    if (rgModel != null)
                    {
                        _ = rg.DeleteAsync();
                    }
                }
            }
        }
    }
}
