using Microsoft.EntityFrameworkCore;
using TestTask;

public class TestTaskDbContext : DbContext
{
    public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options)
        : base(options)
    {
    }

    public DbSet<CustomTask> Tasks { get; set; }
}