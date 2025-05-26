using System;

namespace EventApi.Models
{
public class Event
{
    public virtual string Id { get; set; } = default!;
    public virtual string Name { get; set; } = default!;
    public virtual DateTime StartsOn { get; set; }
    public virtual DateTime EndsOn { get; set; }
    public virtual string Location { get; set; } = default!;
}
}