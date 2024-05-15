using cosmo_db_test.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace cosmo_db_test.Services.Implementations
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly AzureCosmosDbConnectionSettgings dbSettings;
        public DatabaseConnection(IOptionsSnapshot<AzureCosmosDbConnectionSettgings> dbSettings)
        {
            this.dbSettings = dbSettings.Value;
        }
        public async Task<Tuple<Container, string>> DatabaseConnectAsync(string table, string partitionKey)
        {
            var url = dbSettings.AccountUri!;
            var authToken = dbSettings.AccountPrimaryKey!;
            var _database = dbSettings.DbName!;
            var containerName = $"{table}s";

            using CosmosClient client = new( accountEndpoint: url, authKeyOrResourceToken: authToken);
            Database database = await client.CreateDatabaseIfNotExistsAsync(id: _database, throughput: 400);
            await database.CreateContainerIfNotExistsAsync(id: containerName, partitionKeyPath: partitionKey);

            Container container = client.GetContainer(dbSettings.DbName!, containerName);

            return new Tuple<Container, string>(container, containerName);
        }
    }
}