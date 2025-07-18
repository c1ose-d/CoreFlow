namespace CoreFlow.Infrastructure.Configuration;

public class ServerConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> entityTypeBuilder)
    {
        _ = entityTypeBuilder
            .HasKey(keyExpression => keyExpression.Id);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.Id);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.IpAddress)
            .IsRequired()
            .HasMaxLength(100);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.HostName)
            .HasMaxLength(200);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.UserName)
            .IsRequired()
            .HasMaxLength(50);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.Password)
            .IsRequired();

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.ServerBlockId)
            .IsRequired();

        _ = entityTypeBuilder
            .HasIndex(indexExpression => indexExpression.ServerBlockId);

        _ = entityTypeBuilder
            .HasIndex(indexExpression => new { indexExpression.IpAddress, indexExpression.ServerBlockId });

        _ = entityTypeBuilder
            .HasOne(navigationExpression => navigationExpression.ServerBlock)
            .WithMany(navigationExpression => navigationExpression.Servers)
            .HasForeignKey(foreignKeyExpression => foreignKeyExpression.ServerBlockId);
    }
}