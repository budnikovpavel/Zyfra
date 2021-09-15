using System.ComponentModel.DataAnnotations;

namespace Zyfra.Model
{
    /// <summary>
    /// Пол
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Мужской
        /// </summary>
        [Display(Name = "Мужской")]
        Man = 1,
        /// <summary>
        /// Женский
        /// </summary>
        [Display(Name = "Женский")] 
        Woman = 2
    }
}