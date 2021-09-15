using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zyfra.Model;

namespace Zyfra.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс базового репозитория
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<in TId, TEntity>
        where TEntity : EntityBase<TId>
        where TId : struct
    {
        /// <summary>
        /// Возвращает полный список сущностей
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Список сущностей</returns>
        public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        /// <summary>Возвращает сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Сущность</returns>
        public Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавление новой сущности
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Изменение сущности
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Сущность</returns>
        public Task EditAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Сущность</returns>
        public Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    }
}
