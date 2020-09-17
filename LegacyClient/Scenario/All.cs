using Azure.ResourceManager.Resources;
using System;
using System.Linq;

namespace client
{
    class All : Scenario
    {
        public override void Execute()
        {
            var client = new ResourcesManagementClient(Context.SubscriptionId, Context.Credential);
            var list = Enum.GetValues(typeof(Scenarios)).Cast<Scenarios>().ToList();
            try
            {
                foreach(var scenario in list)
                {
                    if (scenario != Scenarios.All)
                    {
                        var executable = ScenarioFactory.GetScenario(scenario);
                        executable.Execute();
                    }
                }
            }
            finally
            {
                foreach (var rgId in CleanUp)
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
