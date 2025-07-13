namespace CoreFlow.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entityTypeBuilder)
    {
        _ = entityTypeBuilder
            .HasKey(keyExpression => keyExpression.Id);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.Id);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.LastName)
            .IsRequired()
            .HasMaxLength(50);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.MiddleName)
            .HasMaxLength(50);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.UserName)
            .IsRequired()
            .HasMaxLength(50);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.Password)
            .IsRequired()
            .HasMaxLength(50);

        _ = entityTypeBuilder
            .Property(propertyExpression => propertyExpression.IsAdmin)
            .IsRequired();

        _ = entityTypeBuilder
            .HasIndex(indexExpression => indexExpression.UserName)
            .IsUnique();

        _ = entityTypeBuilder
            .HasMany(navigationExpression => navigationExpression.UserAppSystems)
            .WithOne(navigationExpression => navigationExpression.User)
            .HasForeignKey(navigationExpression => navigationExpression.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = entityTypeBuilder
            .Navigation(navigationExpression => navigationExpression.UserAppSystems)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}