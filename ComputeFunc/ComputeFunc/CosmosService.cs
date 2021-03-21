using System;
using System.Threading.Tasks;
using ComputeFunc.Models;
using Microsoft.Azure.Cosmos;

namespace ComputeFunc
{
    public static class CosmosService
    {
        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = "https://mikcosmos.documents.azure.com:443/";
        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "sGeHF5rOHUjX0kPNpscBiewW8wgfsqH3D9H04KKk2TrRohXTZv7eyaIqsTauKxrwuWzNxvDpzDjkUC89Wdkjxg==";

        // The Cosmos client instance
        private static CosmosClient cosmosClient;

        // The database we will create
        private static Database database;

        // The container we will create.
        private static Container container;

        // The name of the database and container we will create
        private static string databaseId = "Post";
        private static string containerId = "PostContainer";

        static CosmosService()
        {
            // Create a new instance of the Cosmos Client
            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

            CreateDatabaseAsync().GetAwaiter().GetResult();

            CreateContainerAsync().GetAwaiter().GetResult();
        }


        /// <summary>
        /// Create the database if it does not exist
        /// </summary>
        private static async Task CreateDatabaseAsync()
        {
            // Create a new database
            database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).GetAwaiter().GetResult();
            Console.WriteLine("Created Database: {0}\n", database.Id);
        }

        /// <summary>
        /// Create the container if it does not exist. 
        /// Specifiy "/LastName" as the partition key since we're storing family information, to ensure good distribution of requests and storage.
        /// </summary>
        /// <returns></returns>
        private static async Task CreateContainerAsync()
        {
            // Create a new container
            container = database.CreateContainerIfNotExistsAsync(containerId, "/PostId").GetAwaiter().GetResult();
            Console.WriteLine("Created Container: {0}\n", container.Id);
        }

        /// <summary>
        /// Add Post item to the container
        /// </summary>
        public static async Task AddItemToContainerAsync(Post post)
        {
            // Create an item in the container.
            post.id = Guid.NewGuid().ToString();

            var response = await container.CreateItemAsync<Post>(post, new PartitionKey(post.PostId));

            // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", response.Resource.PostId, response.RequestCharge);

        }

        /// <summary>
        /// Delete an item in the container
        /// </summary>
        public static async Task DeleteItemAsync()
        {
            var partitionKeyValue = "Wakefield";
            var familyId = "Wakefield.7";

            // Delete an item. Note we must provide the partition key value and id of the item to delete
            var wakefieldFamilyResponse = await container.DeleteItemAsync<Post>(familyId, new PartitionKey(partitionKeyValue));
            Console.WriteLine("Deleted Family [{0},{1}]\n", partitionKeyValue, familyId);
        }
    }
}
