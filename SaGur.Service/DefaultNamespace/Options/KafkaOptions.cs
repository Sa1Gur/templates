namespace Some.Root.DefaultNamespace.Options;

public record KafkaOptions
{
    public required string GroupId { get; init; }
    public required string ConnectionString { get; init; }
    public required string Topic { get; init; }
}
