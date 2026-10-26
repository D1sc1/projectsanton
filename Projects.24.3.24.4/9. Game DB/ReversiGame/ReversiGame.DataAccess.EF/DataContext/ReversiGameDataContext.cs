using ReversiGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGame.DataAccess.EF.DataContext
{
    public class ReversiGameDataContext : DbContext
    {
        static ReversiGameDataContext()
        {
            // Если БД еще нет, она создается по модели при первом обращении.
            // Для изменения структуры существующей БД используются миграции: Add-Migration / Update-Database (см. Notes.txt)
            Database.SetInitializer(new CreateDatabaseIfNotExists<ReversiGameDataContext>());
        }

        public ReversiGameDataContext() : base("EFMain")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Cell> Cells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
