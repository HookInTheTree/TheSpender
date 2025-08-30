using TheSpender.DAL.Entities.Categories;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Operations
{
    /// <summary>
    /// Финансовая операция (доход или расход).
    /// </summary>
    public class Operation : BaseEntity
    {
        /// <summary>
        /// Идентификатор владельца категории
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Владелец категории
        /// </summary>
        public required User User { get; set; }

        /// <summary>
        /// Описание заданное пользователем, при записи операции
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// дата записи операции
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Сумма указанная в операции
        /// </summary>
        public decimal SumOfMoney { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Категория операции
        /// </summary>
        public required Category Category { get; set; }

        /// <summary>
        /// Численный идентификатор операции
        /// Инкрементируется для каждого пользователя независимо.
        /// </summary>
        public long OperationNumber { get; set; }
    }
}
