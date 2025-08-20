namespace TheSpender.DAL.Entities.Categories
{
    /// <summary>
    /// Категория для группировки операций (например: еда, зарплата).
    /// </summary>
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid? UserId { get; set; }
        public bool IsDefault { get; set; }
        public CategoryTypes CategoryType { get; set; }

    }
}
