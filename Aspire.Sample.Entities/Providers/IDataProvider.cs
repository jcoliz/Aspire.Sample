namespace Aspire.Sample.Providers;

/// <summary>
/// Defines a services to provide data into the system
/// </summary>
public interface IDataProvider
{
    /// <summary>
    /// Retrieves a querable set of <typeparamref name="TEntity"/> objects
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being operated on by this set.</typeparam>
    /// <returns>querable set of <typeparamref name="TEntity"/> objects</returns>
    IQueryable<TEntity> Set<TEntity>() where TEntity : class;
}
