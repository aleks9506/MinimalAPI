using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Infrasctructure.Data.Entities;

namespace ToDo.Infrasctructure.Data.Configuration
{
    public class ToDoTaskConfiguration :
        IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            builder.ToTable("ToDoTasks");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Task)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(t => t.Created)
                .IsRequired();
            builder.Property(t => t.Deleted)
                .IsRequired(false);
        }
    }
}
