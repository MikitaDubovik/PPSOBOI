using System;
using System.Collections.Generic;
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

        private static readonly int coefficient = 2;

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
        /// First call to service will call constructor
        /// </summary>
        public static void SetUp()
        {

        }

        /// <summary>
        /// Add Post item to the container
        /// </summary>
        public static async Task<string> AddItemToContainerAsync(Post post)
        {
            // Create an item in the container.
            post.id = Guid.NewGuid().ToString().Split("-")[0] + post.PostId.Value;

            var response = await container.CreateItemAsync<Post>(post, new PartitionKey(post.PostId.Value));

            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", response.Resource.PostId, response.RequestCharge);

            return post.id;
        }

        /// <summary>
        /// Delete an item in the container
        /// </summary>
        public static async Task DeleteItemAsync(string itemId, string partitionKeyValue)
        {
            try
            {
                // Delete an item. Note we must provide the partition key value and id of the item to delete
                var wakefieldFamilyResponse = await container.DeleteItemAsync<Post>(partitionKeyValue, new PartitionKey(partitionKeyValue));
                Console.WriteLine("Deleted item [{0},{1}]\n", partitionKeyValue, itemId);
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task PreprocessItemAsync(Post post)
        {
            Calculate(float.Parse(post.Price));
        }

        private static float Calculate(float price) => price * coefficient;

        private static string UpdateName(string name) => name += "#Cont";

    }
}
