using System.ComponentModel.DataAnnotations;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Categories
{
    /// <summary>
    /// Категория для группировки операций (например: еда, зарплата).
    /// </summary>
    public class Category : BaseEntity
    {
        public required string Name { get; set; }

        /// <summary>
        /// Идентификатор владельца категории
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Владелец категории
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Является ли категория дефолтной. Дефолтная категория не должна принадлежать никому.
        /// Из них генерируется дефолтный набор категорий для пользователя
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Тип категории
        /// </summary>
        public CategoryTypes CategoryType { get; set; }

        [Timestamp]
        public uint Version { get; set; }
    }
}
