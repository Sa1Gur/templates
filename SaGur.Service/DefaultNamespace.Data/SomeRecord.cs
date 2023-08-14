using NodaTime;

namespace Some.Root.DefaultNamespace.Data;
public class SomeRecord
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    public Instant ModifiedInstant { get; set; }
}
