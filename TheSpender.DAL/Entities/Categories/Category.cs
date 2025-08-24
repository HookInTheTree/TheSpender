using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Categories
{
    /// <summary>
    /// Категория для группировки операций (например: еда, зарплата).
    /// </summary>
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public bool IsDefault { get; set; }
        public CategoryTypes CategoryType { get; set; }

    }
}
