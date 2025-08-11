namespace TheSpender.DAL.Entities
{
    public enum CategoryTypes
    {
        Income,
        Expense
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public CategoryTypes CategoryType { get; set; }
    }
}
