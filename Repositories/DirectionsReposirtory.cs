using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyfra.DbModel;
using Zyfra.Repositories.Abstractions;
using Zyfra.Repositories.Interfaces;

namespace Zyfra.Repositories
{
    public class DirectionsRepository : Repository<int, Direction>, IDirectionsRepository
    {
        public DirectionsRepository(ZyfraDbContext context) : base(context)
        {
        }

        /// <summary>Возвращает список дочерних подразделений/>
        /// </summary>
        /// <param name="parentId">Идентификатор головного подразделения</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        public async Task<IReadOnlyList<Direction>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken = default) =>
         await DbSet.Where(x => x.ParentDirectionId == parentId).ToListAsync(cancellationToken);

        /// <summary>
        /// Проверяет возможность наличия у подразделения родительского узла (в базе должно быть как минимум 1 подразделение)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> CanHasParentDirectionAsync(CancellationToken cancellationToken = default) =>
            await DbSet.AnyAsync(cancellationToken);

    }


}
