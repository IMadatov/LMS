using Microsoft.EntityFrameworkCore;
using Test.Domain.Entities;

namespace Test.Infrastucture;

public class TestsContext: DbContext
{

    
    public TestsContext(DbContextOptions<TestsContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TestModel> Tests=> Set<TestModel>();

    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();
    public DbSet<FileInfos> fileInfos =>Set<FileInfos>();
}
