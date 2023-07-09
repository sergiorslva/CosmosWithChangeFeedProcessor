using Microsoft.Azure.Cosmos;

namespace ChangeFeedConsole
{
    class Program
    {
        private static readonly string _endpointUrl = Environment.GetEnvironmentVariable("CosmosDBEndpoint");
        private static readonly string _primaryKey = Environment.GetEnvironmentVariable("CosmosDBPrimarykey");
        private static readonly string _databaseId = Environment.GetEnvironmentVariable("CosmosDBDatabaseID");
        private static readonly string _containerId = Environment.GetEnvironmentVariable("CosmosDBContainerID");        
        private static CosmosClient _client = new CosmosClient(_endpointUrl, _primaryKey);

        private static Container.ChangesHandler<object> handleChanges = async (             
             IReadOnlyCollection<object> changes,
             CancellationToken cancellationToken
         ) => {
             Console.WriteLine("Backing up items");

             foreach (var doc in changes)
             {
                 Console.WriteLine(doc);
                 await Task.Delay(10);
             }

             Console.WriteLine("Finished handling changes");
         };


        static async Task Main(string[] args)
        {                        
            Database database = _client.GetDatabase(_databaseId);
            Container container = database.GetContainer(_containerId);
            
            ContainerProperties leaseContainerProperties = new ContainerProperties("containerLeases", "/id");
            Container leaseContainer = await database.CreateContainerIfNotExistsAsync(leaseContainerProperties);

            var builder = container.GetChangeFeedProcessorBuilder("migrationProcessor", handleChanges);
        
            var processor = builder
                            .WithInstanceName("changeFeedConsole")                            
                            .WithLeaseContainer(leaseContainer)
                            .Build();

            await processor.StartAsync();
            Console.WriteLine("Started Change Feed Processor");
            Console.WriteLine("Press any key to stop the processor...");

            Console.ReadKey();

            Console.WriteLine("Stopping Change Feed Processor");
            await processor.StopAsync();
        }
    }
}