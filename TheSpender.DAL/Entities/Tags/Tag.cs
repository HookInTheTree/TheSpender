namespace TheSpender.DAL.Entities.Tags
{
    /// <summary>
    /// Тег для детализации категорий (например: кофе, суши для категории еда).
    /// </summary>
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
