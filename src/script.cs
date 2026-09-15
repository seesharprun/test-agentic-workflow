#:package Azure.Identity@1.*
#:package Newtonsoft.Json@13.*
#:package Microsoft.Azure.Cosmos@3.*

using Azure.Identity;
using Microsoft.Azure.Cosmos;

DefaultAzureCredential credential = new();

CosmosClient client = new(
    accountEndpoint: "<azure-cosmos-db-nosql-account-endpoint>",
    tokenCredential: credential
);

Database database = client.GetDatabase("cosmicworks");

Container container = database.GetContainer("products");

Product item = new(
    id: "aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb",
    category: "gear-surf-surfboards",
    name: "Yamba Surfboard",
    quantity: 12,
    price: 850.00m,
    clearance: false
);

ItemResponse<Product> upsertedItemResponse = await container.UpsertItemAsync<Product>(
    item: item,
    partitionKey: new PartitionKey("gear-surf-surfboards")
);

ItemResponse<Product> readItemResponse = await container.ReadItemAsync<Product>(
    id: "aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb",
    partitionKey: new PartitionKey("gear-surf-surfboards")
);

string query = "SELECT * FROM products p WHERE p.category = @category";

var queryDefinition = new QueryDefinition(query)
  .WithParameter("@category", "gear-surf-surfboards");

using FeedIterator<Product> feed = container.GetItemQueryIterator<Product>(
    queryDefinition
);

List<Product> items = [];
while (feed.HasMoreResults)
{
    FeedResponse<Product> feedResponse = await feed.ReadNextAsync();
    foreach (Product feedItem in feedResponse)
    {
        items.Add(feedItem);
    }
}

public record Product(
    string id,
    string category,
    string name,
    int quantity,
    decimal price,
    bool clearance
);