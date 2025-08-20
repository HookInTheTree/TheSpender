namespace TheSpender.DAL.Entities.Operations
{
    /// <summary>
    /// Финансовая операция (доход или расход).
    /// </summary>
    public class Operation : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public int SumOfMoney { get; set; }
        public Guid CategoryId { get; set; }
        public int OperationNumber { get; set; }
    }
}
