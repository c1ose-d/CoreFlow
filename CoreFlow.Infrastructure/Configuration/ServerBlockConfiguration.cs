namespace CoreFlow.Infrastructure.Configuration;

public class ServerBlockConfiguration : IEntityTypeConfiguration<ServerBlock>
{
    public void Configure(EntityTypeBuilder<ServerBlock> entityTypeBuilder)
    {
        _ = entityTypeBuilder
            .HasKey(keyExpression => keyExpression.Id);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.Id);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.Name)
            .IsRequired()
            .HasMaxLength(200);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.AppSystemId)
            .IsRequired();

        _ = entityTypeBuilder
            .HasIndex(indexExpression => new { indexExpression.Name, indexExpression.AppSystemId })
            .IsUnique();

        _ = entityTypeBuilder
            .HasOne<AppSystem>()
            .WithMany()
            .HasForeignKey(foreignKeyExpression => foreignKeyExpression.AppSystemId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = entityTypeBuilder
            .HasMany(navigationExpression => navigationExpression.Servers)
            .WithOne(navigationExpression => navigationExpression.ServerBlock)
            .HasForeignKey(foreignKeyExpression => foreignKeyExpression.ServerBlockId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}