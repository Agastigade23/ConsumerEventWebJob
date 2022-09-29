using Azure.Messaging.EventHubs.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerEventWebJob
{
    public class Consumer : IConsumer
    {
        string connectionString = "Endpoint=sb://iaminazurenamespace.servicebus.windows.net/;SharedAccessKeyName=Demo1eventhub;SharedAccessKey=05Qpha3BwB73tjgAz8H3gKKK2HDbfawN5qRGNq9iFqI=;EntityPath=demo1";
        string eventHubName = "demo1";
        //Read all events. No blol container needed directly consumer can use the method to procees event
        public async Task ConsumerChargeEvent(string consumerGroup)
        {
            try
            {
                CancellationTokenSource cancellationSource = new CancellationTokenSource();
                //cancellationSource.CancelAfter(TimeSpan.FromSeconds(30));
                EventHubConsumerClient eventConsumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName);

                await foreach (PartitionEvent partitionEvent in eventConsumer.ReadEventsAsync(cancellationSource.Token))
                {
                    Console.WriteLine("---Execution from ConsumerReadEvent method---");
                    Console.WriteLine("------");
                    Console.WriteLine("Event Data recieved {0} ", Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray()));
                    if (partitionEvent.Data != null)
                    {
                        Console.WriteLine("Event Data {0} ", Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray()));
                        if (partitionEvent.Data.Properties != null)
                        {
                            foreach (var keyValue in partitionEvent.Data.Properties)
                            {
                                Console.WriteLine("Event data key = {0}, Event data value = {1}", keyValue.Key, keyValue.Value);
                            }
                        }
                    }
                }

                Console.WriteLine("ConsumerReadEvent end");
                await Task.CompletedTask;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error occruied {0}. Try again later", exp.Message);
            }
        }

        public async Task ConsumerReadEvent(string Default )
        {
           ConsumerChargeEvent(Default).Wait();
        }
    }
}
