using Amazon.DynamoDBv2.DataModel;

namespace GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;

[DynamoDBTable("globo-clima-platform")]
public class GloboClimaPlatformDynamoDbModel
{
    [DynamoDBHashKey("pk")] public string PartitionKey { get; set; }

    [DynamoDBRangeKey("sk")] public string SortKey { get; set; }

    [DynamoDBProperty] public List<string> Cities { get; set; } = new List<string>();

    [DynamoDBProperty] public List<string> Countries { get; set; } = new List<string>();
}