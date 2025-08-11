namespace TheSpender.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
