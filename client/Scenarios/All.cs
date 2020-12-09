﻿using azure_proto_core;
using System;
using System.Linq;

namespace client
{
    class All : Scenario
    {
        public override void Execute()
        {
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
                    ResourceIdentifier id = new ResourceIdentifier(rgId);
                    var rg = new ArmClient().ResourceGroup(rgId);
                    Console.WriteLine($"--------Deleting {rg.Id.Name}--------");
                    try
                    {
                        _ = rg.DeleteAsync();
                    }
                    catch
                    {
                        //ignore exceptions in case rg doesn't exist
                    }
                }
            }
        }
    }
}
