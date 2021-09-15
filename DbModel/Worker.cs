using System;
using System.ComponentModel.DataAnnotations;
using Zyfra.Model;

namespace Zyfra.DbModel
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    [Display(Name = "Сотрудник")]
    public class Worker
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [Display(Name = "ID")] 
        public int Id { get; set; }
        /// <summary>
        /// ФИО
        /// </summary>
        [Display(Name = "ФИО")]
        [Required]
        public string Fio { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        [Display(Name = "Пол")]
        [Required]
        public Gender Gender { get; set; }
        /// <summary>
        /// Профессия
        /// </summary>
        [Display(Name = "Профессия")]
        [Required]
        public string Profession { get; set; }
        /// <summary>
        /// Наличие водительских прав
        /// </summary>
        [Display(Name = "Наличие водительских прав")]
        public bool IsDriver { get; set; }
        /// <summary>
        /// Идентификатор подразделения, которому принадлежит сотрудник
        /// </summary>
        [Display(Name = "Подразделение ")]
        public int DirectionId { get; set; }
        /// <summary>
        /// Подразделение которому принадлежит сотрудник
        /// </summary>
        [Display(Name = "Подразделение ")]
        public Direction Direction { get; set; }
    }
}
