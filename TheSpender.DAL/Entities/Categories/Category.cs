namespace TheSpender.DAL.Entities.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int? UserId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public CategoryTypes CategoryType { get; set; }

    }
}
