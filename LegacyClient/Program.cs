using Azure.ResourceManager.Resources;
using client;
using System;
using System.Linq;

namespace LegacyClient
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
                var client = new ResourcesManagementClient(scenario.Context.SubscriptionId, scenario.Context.Credential);
                foreach (var rgId in Scenario.CleanUp)
                {
                    var name = GetResourceName(rgId);
                    try
                    {
                        var rg = client.ResourceGroups.Get(name).Value;
                        if (rg != null)
                        {
                            Console.WriteLine($"--------Deleting {rg.Name}--------");
                            _ = client.ResourceGroups.StartDelete(rg.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                        }
                    }
                    catch
                    {
                        // ignore errors in get/delete
                    }
                }
            }
        }

        static string GetResourceName(string resourceId)
        {
            return resourceId?.Split('/', StringSplitOptions.RemoveEmptyEntries)?.Last();
        }

    }
}
