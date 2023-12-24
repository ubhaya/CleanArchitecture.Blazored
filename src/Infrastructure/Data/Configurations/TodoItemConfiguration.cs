using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Blazored.Domain.Entities;

namespace CleanArchitecture.Blazored.Infrastructure.Data.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(280)
            .IsRequired();

        builder.Property(t => t.Note)
            .HasMaxLength(4000);
    }
}
