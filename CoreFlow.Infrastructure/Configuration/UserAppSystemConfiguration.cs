namespace CoreFlow.Infrastructure.Configuration;

public class UserAppSystemConfiguration : IEntityTypeConfiguration<UserAppSystem>
{
    public void Configure(EntityTypeBuilder<UserAppSystem> entityTypeBuilder)
    {
        entityTypeBuilder
            .HasKey(keyExpression => new { keyExpression.UserId, keyExpression.AppSystemId });

        entityTypeBuilder
            .HasOne(navigationExpression => navigationExpression.User)
            .WithMany(navigationExpression => navigationExpression.UserAppSystems)
            .HasForeignKey(navigationExpression => navigationExpression.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entityTypeBuilder
            .HasOne(navigationExpression => navigationExpression.AppSystem)
            .WithMany("_userAppSystems")
            .HasForeignKey(navigationExpression => navigationExpression.AppSystemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}