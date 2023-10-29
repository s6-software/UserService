using Microsoft.EntityFrameworkCore;

namespace API.Models.User
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> users{ get; set; } = null!;
    }
}
