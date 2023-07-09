using CosmosSample.Interfaces;
using CosmosSample.Models;
using CosmosSample.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CosmosDB
builder.Services.AddSingleton<ICosmosSampleService>(options =>
{

    string url = builder.Configuration.GetValue<string>("AzureCosmosDbSettings:URL");
    string primaryKey = builder.Configuration.GetValue<string>("AzureCosmosDbSettings:PrimaryKey");
    string dbName = builder.Configuration.GetValue<string>("AzureCosmosDbSettings:DatabaseName");    
    string containerName = builder.Configuration.GetValue<string>("AzureCosmosDbSettings:ContainerName");
    string leaseContainerName = builder.Configuration.GetValue<string>("AzureCosmosDbSettings:LeasesContainerName");

    var cosmosClient = new CosmosClient(
        url,
        primaryKey
    );

    return new CosmosSampleService(cosmosClient, dbName, containerName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
