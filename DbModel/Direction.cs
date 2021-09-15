using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zyfra.Model;

namespace Zyfra.DbModel
{
    /// <summary>
    /// Подразделение
    /// </summary>
    public sealed class Direction : EntityBase<int>
    {
        public Direction()
        {
            Workers = new HashSet<Worker>();
            DateCreated = DateTime.Now;
            ParentDirectionId = 0;
        }

        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name= "Наименование")]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [Display(Name = "Дата создания")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; protected set; }
        /// <summary>
        /// Описание
        /// </summary>
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [MaxLength(250)]
        public string Description { get; set; }
        /// <summary>
        /// Список сотрудников 
        /// </summary>
        public ICollection<Worker> Workers { get; set; }

        /// <summary>
        /// Идентификатор головного подразделения
        /// </summary>
        [Display(Name = "Головное подразделение")]
        [Required(AllowEmptyStrings = true)]
        public int ParentDirectionId { get; set; }
        /// <summary>
        /// Головное подразделение
        /// </summary>
        public Direction ParentDirection { get; set; }
        public override string ToString() => $"{Name}";
    }
}
