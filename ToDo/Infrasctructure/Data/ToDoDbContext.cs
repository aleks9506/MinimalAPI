using System;
using System.Collections.Generic;
using System.Text;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ToDo.Infrasctructure.Data.Entities;

namespace ToDo.Infrasctructure.Data
{
    public class ToDoDbContext : DbContext
    {
        public required DbSet<ToDoTask> ToDoTasks { get; set; }
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
        }
    }
}
