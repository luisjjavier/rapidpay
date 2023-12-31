namespace Core
{
    public abstract  class BaseEntity
    {
        /// <summary>
        /// Entity Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The date when this entity was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The user who initially created this entity (optional).
        /// It is recommended to use this format for uniformity: DisplayName | Email
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The last date when this entity was updated (optional).
        /// If the value is empty, it means that this entity has never been updated.
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// The last user who updated this entity (optional).
        /// It is recommended to use this format for uniformity: DisplayName | Email
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// A random value that must change whenever an entity is persisted
        /// </summary>
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
