using Microsoft.Azure.Cosmos;

namespace Capital_Placement_Test.Services
{
    public interface IDatabaseConnection
    {
        Task<Tuple<Container, string>> DatabaseConnectAsync(string table, string partitionKey);
    }
}