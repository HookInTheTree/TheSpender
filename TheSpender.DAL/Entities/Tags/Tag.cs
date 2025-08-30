using System.ComponentModel.DataAnnotations;
using TheSpender.DAL.Entities.Categories;

namespace TheSpender.DAL.Entities.Tags
{
    /// <summary>
    /// Тег (лексема) для описания категорий (например: кофе, суши для категории еда).
    /// </summary>
    public class Tag : BaseEntity
    {
        public required string Name { get; set; }

        public Guid CategoryId { get; set; }

        public required Category Category { get; set; }

        [Timestamp]
        public uint Version { get; set; }
    }
}
