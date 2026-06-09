namespace MyApp.Domain.Entities;
public abstract class BaseId<TKey>
{
    public TKey Id { get ; private set ; } = default!;
}