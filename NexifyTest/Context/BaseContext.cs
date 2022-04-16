namespace NexifyTest.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {

        }

        public DbSet<UserInfos> UserInfos { get; set; } = null!;
    }
}
