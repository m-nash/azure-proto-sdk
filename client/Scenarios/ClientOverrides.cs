using azure_proto_compute;
using azure_proto_core;
using System;
using Azure.Core;
using Azure.ResourceManager.Resources;
using Azure.Core.Pipeline;
using System.Threading;
using System.Diagnostics;

namespace client
{
    class ClientOptionsOverride : Scenario
    {
        public override void Execute()
        {

            ArmClientOptions options1 = new ArmClientOptions();
            ArmClientOptions options2 = new ArmClientOptions();
            var dummyPolicy1 = new dummyPolicy();
            var dummyPolicy2 = new dummyPolicy();
            options1.AddPolicy(dummyPolicy1, HttpPipelinePosition.PerCall);
            options2.AddPolicy(dummyPolicy2, HttpPipelinePosition.PerCall);
            var client1 = new ArmClient(options1);
            var client2 = new ArmClient(options2);

            foreach (var sub in client1.Subscriptions().List())
            {
                var x = sub;
            }
            
            Debug.Assert(dummyPolicy1.numMsgGot != dummyPolicy2.numMsgGot);
            Console.WriteLine("\nPASSED\n");


        }

        private class dummyPolicy : HttpPipelineSynchronousPolicy
        {
            public int numMsgGot = 0;

            public override void OnReceivedResponse(HttpMessage message)
            {
                Interlocked.Increment(ref numMsgGot);
            }
        }
    }
}
