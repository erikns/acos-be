using System.Collections.Generic;
using ACOS_be.Entities;
using ACOS_be.Models;
using Microsoft.EntityFrameworkCore;

namespace ACOS_be.Data
{
    public interface Repository
    {
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }
        int SaveAll();
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;
    }

    public class ApplicationContext : DbContext, Repository
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {}

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Task");
            modelBuilder.Entity<User>().ToTable("User");
        }

        int Repository.SaveAll()
        {
            return SaveChanges();
        }

        TEntity Repository.Add<TEntity>(TEntity entity)
        {
            var added = Add(entity);
            return added.Entity;
        }

        void Repository.Remove<TEntity>(TEntity entity)
        {
            Remove(entity);
        }

        TEntity Repository.Update<TEntity>(TEntity entity)
        {
            return Update(entity).Entity;
        }
    }
}