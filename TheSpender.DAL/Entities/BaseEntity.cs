namespace TheSpender.DAL.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }

        /// <summary>
        /// Флаг удаления записи в таблице
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
