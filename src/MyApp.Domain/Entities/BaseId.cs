namespace MyApp.Domain.Entities;
public abstract class BaseId<TKey>
{
    public TKey Id { get ; private set ; } = default!;
    public bool IsDeleted { get; set; } = false; 
    public DateTime? DeletedAt { get; set; }
}