using EmailSender.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<Message> Messages { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration config)
            :base(options)
        {
            this.config = config;
        }
    }
}