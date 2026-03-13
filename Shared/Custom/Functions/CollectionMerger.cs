namespace Shared.Custom.Functions;

public static class CollectionMerger
{
    /// <summary>
    /// Merges a collection of incoming DTOs into an existing entity collection.
    /// Updates existing entities, removes entities not present in the incoming collection,
    /// and adds new entities for DTOs that don't already exist.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity in the existing collection.
    /// </typeparam>
    /// <typeparam name="TDto">
    /// The type of the DTO in the incoming collection.
    /// </typeparam>
    /// <param name="existing">
    /// The existing collection of entities to merge into. Entities not present in the incoming
    /// collection will be removed.
    /// </param>
    /// <param name="incoming">
    /// The incoming collection of DTOs representing the desired state of the collection.
    /// </param>
    /// <param name="getEntityKey">
    /// A function to extract the unique key from an entity. Used to match DTOs to entities.
    /// </param>
    /// <param name="getDtoKey">
    /// A function to extract the unique key from a DTO. Used to match DTOs to entities.
    /// </param>
    /// <param name="createEntity">
    /// A factory function to create a new entity from a DTO if no matching entity exists.
    /// </param>
    /// <param name="updateEntity">
    /// An action to update an existing entity based on a matching DTO.
    /// </param>
    public static void MergeCollection<TEntity, TDto>(
        ICollection<TEntity> existing,
        IEnumerable<TDto> incoming,
        Func<TEntity, Guid> getEntityKey,
        Func<TDto, Guid> getDtoKey,
        Func<ICollection<TDto>, ICollection<TEntity>> createEntity,
        Action<TDto, TEntity> updateEntity)
        where TEntity : class
        where TDto : class
    {
        // Keep track of incoming items that have not been matched yet
        var incomingList = incoming.ToList();

        // Remove entities that no longer exist and update matched ones
        foreach (var entity in existing.ToList())
        {
            var dto = incomingList.FirstOrDefault(d => getDtoKey(d) == getEntityKey(entity));
            if (dto != null)
            {
                updateEntity(dto, entity);  // update existing entity
                incomingList.Remove(dto);    // remove matched DTOs
            }
            else
            {
                existing.Remove(entity);     // remove missing entities
            }
        }
        
        var entities = createEntity(incomingList);
        foreach (var entity in entities)
        {
            existing.Add(entity);
        }
    }
}