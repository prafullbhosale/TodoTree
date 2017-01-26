using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoModels.Models;

namespace TodoTree.Data
{
    public class TodoTreeContext : DbContext
    {
        public TodoTreeContext (DbContextOptions<TodoTreeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoNodeParentMap>(p =>
            {
                p.HasKey(nameof(TodoModels.Models.TodoNodeParentMap.TodoNodeParentMapId));
                p.Property(e => e.TodoNodeId)
                    .HasDefaultValue(-1);
            });

            foreach(var rel in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                rel.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<TodoModels.Models.TodoNode> TodoNode { get; set; }

        public DbSet<TodoModels.Models.TodoNodeParentMap> TodoNodeParentMap { get; set; }
    }
}
