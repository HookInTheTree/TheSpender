namespace TheSpender.DAL.Entities.Tags
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
