using TheSpender.DAL.Entities.Categories;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Operations
{
    /// <summary>
    /// Финансовая операция (доход или расход).
    /// </summary>
    public class Operation : BaseEntity
    {
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal SumOfMoney { get; set; }
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        public long OperationNumber { get; set; }
    }
}
