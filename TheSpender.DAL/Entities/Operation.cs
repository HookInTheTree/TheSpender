namespace TheSpender.DAL.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public int SumOfMoney { get; set; }
        public int CategoryId { get; set; }
        public int OperationNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
