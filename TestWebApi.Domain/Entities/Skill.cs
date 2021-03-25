namespace TestWebApi.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The skill.
    /// </summary>
    public class Skill : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
