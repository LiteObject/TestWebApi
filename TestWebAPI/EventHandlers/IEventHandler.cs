namespace TestWebAPI.EventHandlers
{

    using TestWebApi.Domain.Entities;

    /// <summary>
    /// The base event handler.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IEventHandler<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Gets or sets the on save.
        /// </summary>
        // Action<IEnumerable<EntityEntry<T>>> OnSave { get; set; }
    }
}
