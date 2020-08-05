using azure_proto_core;
using azure_proto_management;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace client
{
    class All : Scenario
    {
        public override void Execute()
        {
            var list = Enum.GetValues(typeof(Scenarios)).Cast<Scenarios>().ToList();
            try
            {
                Parallel.ForEach(list, (scenario) =>
                {
                    if (scenario == Scenarios.All)
                    {
                        var executable = ScenarioFactory.GetScenario(scenario);
                        executable.Execute();
                    }
                });
            }
            finally
            {
                foreach (var rgId in CleanUp)
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
