using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyfra.DbModel;
using Zyfra.Model;
using Zyfra.Repositories.Interfaces;

namespace Zyfra.Repositories.Abstractions
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public abstract class Repository<TId, TEntity> : IRepository<TId, TEntity>
        where TId : struct
        where TEntity : EntityBase<TId>
    {
        protected readonly DbSet<TEntity> DbSet;
        /// <summary>
        /// >Контекст БД
        /// </summary>
        protected readonly ZyfraDbContext DbContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст БД</param>
        /// <exception cref="ArgumentNullException">Отдаст исключение при передаче пустого контекста в качестве параметра</exception>
        protected Repository(ZyfraDbContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = DbContext.Set<TEntity>();
        }


        /// <summary>
        /// Возвращает полный список сущностей
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Список сущностей</returns>
        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await DbSet.ToListAsync<TEntity>(cancellationToken);

        /// <summary>Возвращает сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Сущность</returns>
        public virtual async Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken = default) =>
            await DbSet.FindAsync(new object[]{ id }, cancellationToken);

        /// <summary>
        /// Добавление новой сущности
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <exception cref="ArgumentNullException">Отдаст исключение при передаче пустой сущности в качестве параметра</exception>
        public virtual  async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await DbSet.AddAsync(entity, cancellationToken);

                await DbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Не удалось добавить { entity.ToString() }, ошибка обращения к БД : {e.Message}", e);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Не удалось добавить { entity.ToString() }: {e.Message}", e);
            }
        }


        /// <summary>
        /// Изменение сущности
        /// </summary>
        /// <param name="entity">Редактируемая сущность</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Сущность</returns>
        public virtual async Task EditAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Не удалось изменить { entity }, ошибка обращения к БД : {e.Message}", e);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Не удалось изменить { entity }: {e.Message}", e);
            }
        }


        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Сущность</returns>
        public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(id, cancellationToken);
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                DbSet.Remove(entity);
                await DbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Не удалось изменить { entity }, ошибка обращения к БД : {e.Message}", e);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception($"Не удалось изменить { entity }: {e.Message}", e);
            }

        }
    }
}
