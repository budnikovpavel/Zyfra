namespace Zyfra.Model
{
    /// <summary>Абстрактный класс сущности
    /// </summary>
    /// <typeparam name="TId">Тип идентификационного поля сущности</typeparam>
    public abstract class EntityBase<TId> where TId : struct
    {
        /// <summary>Идентификатор сущности
        /// </summary>
        public TId Id { get; set; }
    }
}