using Microsoft.Azure.Cosmos;

namespace cosmo_db_test.Services
{
    public interface IDatabaseConnection
    {
        Task<Tuple<Container, string>> DatabaseConnectAsync(string table, string partitionKey);
    }
}