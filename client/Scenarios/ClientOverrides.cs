using Azure.ResourceManager.Core;
using System;
using Azure.Core;
using Azure.Core.Pipeline;
using System.Threading;
using System.Diagnostics;

namespace client
{
    class ClientOptionsOverride : Scenario
    {
        public override System.Threading.Tasks.Task Execute()
        {

            AzureResourceManagerClientOptions options1 = new AzureResourceManagerClientOptions();
            AzureResourceManagerClientOptions options2 = new AzureResourceManagerClientOptions();
            var dummyPolicy1 = new dummyPolicy();
            var dummyPolicy2 = new dummyPolicy();
            options1.AddPolicy(dummyPolicy1, HttpPipelinePosition.PerCall);
            options2.AddPolicy(dummyPolicy2, HttpPipelinePosition.PerCall);
            var client1 = new AzureResourceManagerClient(options1);
            var client2 = new AzureResourceManagerClient(options2);

            foreach (var sub in client1.GetSubscriptionContainer().List())
            {
                var x = sub;
            }
            
            Debug.Assert(dummyPolicy1.numMsgGot != dummyPolicy2.numMsgGot);
            Console.WriteLine("\nPASSED\n");
            return System.Threading.Tasks.Task.FromResult<object>(null);
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
