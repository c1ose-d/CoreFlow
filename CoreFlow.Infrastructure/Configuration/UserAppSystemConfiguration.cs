namespace CoreFlow.Infrastructure.Configuration;

public class UserAppSystemConfiguration : IEntityTypeConfiguration<UserAppSystem>
{
    public void Configure(EntityTypeBuilder<UserAppSystem> entityTypeBuilder)
    {
        _ = entityTypeBuilder
            .HasKey(keyExpression => new { keyExpression.UserId, keyExpression.AppSystemId });

        _ = entityTypeBuilder
            .HasOne(navigationExpression => navigationExpression.User)
            .WithMany(navigationExpression => navigationExpression.UserAppSystems)
            .HasForeignKey(navigationExpression => navigationExpression.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = entityTypeBuilder
            .HasOne(navigationExpression => navigationExpression.AppSystem)
            .WithMany("_userAppSystems")
            .HasForeignKey(navigationExpression => navigationExpression.AppSystemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}