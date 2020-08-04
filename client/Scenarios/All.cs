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
            Parallel.ForEach(list, (scenario) =>
            {
                if (scenario != Scenarios.All)
                {
                    var executable = ScenarioFactory.GetScenario(scenario);
                    try
                    {
                        executable.Execute();
                    }
                    finally
                    {
                        foreach (var rgName in executable.CleanUp)
                        {
                            var rg = AzureClient.GetResourceGroup(executable.Context.SubscriptionId, rgName);
                            _ = rg.DeleteAsync();
                        }
                    }
                }
            });
        }
    }
}
