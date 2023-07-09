# CosmosWithChangeFeedProcessor

# Create a CosmosDB database

    [Create a Cosmos database](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/quickstart-portal)


# Run API

## before running the API, set up appsettings.Development.json file according your Cosmos Keys

    ```
    "AzureCosmosDbSettings": {
        "URL": <URI>,
        "PrimaryKey": <PRIMARY KEY>,
        "DatabaseName": <DATABASE NAME>,
        "ContainerName": <CONTAINER NAME>    
    }
    ```  
    dotnet run


# Run Console Application

## before running the Console Application, set up follow environment variables

    - CosmosDBEndpoint
    - CosmosDBPrimarykey
    - CosmosDBDatabaseID
    - CosmosDBContainerID
    
    dotnet run