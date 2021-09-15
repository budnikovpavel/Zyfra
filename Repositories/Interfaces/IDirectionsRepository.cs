using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zyfra.DbModel;

namespace Zyfra.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для подразделений
    /// </summary>
    public interface IDirectionsRepository : IRepository<int, Direction>
    {
        /// <summary>Возвращает список дочерних подразделений/>
        /// </summary>
        /// <param name="parentId">Идентификатор головного подразделения</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        public Task<IReadOnlyList<Direction>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Проверяет возможность наличия у подразделения родительского узла (в базе должно быть как минимум 1 подразделение)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> CanHasParentDirectionAsync(CancellationToken cancellationToken = default);
    }
}
