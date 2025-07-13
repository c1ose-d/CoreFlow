namespace CoreFlow.Infrastructure.Configuration;

public class SystemConfiguration : IEntityTypeConfiguration<AppSystem>
{
    public void Configure(EntityTypeBuilder<AppSystem> entityTypeBuilder)
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
            .Property(propertyExpression => propertyExpression.ShortName)
            .IsRequired()
            .HasMaxLength(50);

        _ = entityTypeBuilder
            .HasIndex(indexExpression => indexExpression.Name)
            .IsUnique();

        _ = entityTypeBuilder
            .HasMany<UserAppSystem>("_userAppSystems")
            .WithOne(us => us.AppSystem)
            .HasForeignKey(us => us.AppSystemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}