using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Zyfra.Model;

namespace Zyfra.DbModel
{
    public class ZyfraDbContext:DbContext
    {

        public ZyfraDbContext()
        {
        }

        public ZyfraDbContext(DbContextOptions<ZyfraDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// Подразделения 
        /// </summary>
        public virtual DbSet<Direction> Directions { get; set; }
        /// <summary>
        /// Сотрудники
        /// </summary>
        public virtual DbSet<Worker> Workers { get; set; }
        /// <summary>
        /// Конфигурирование соединения
        /// </summary>
        /// <param name="optionsBuilder">Опции для настройки соединенияЫ</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=.\\sqlexpress;initial catalog=ZyfraDb;persist security info=True;user id=super;password=dm47cess;multipleactiveresultsets=True;");
            }
        }

    }
}
