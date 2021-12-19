namespace IncludeTypeBackend.Services;

public class PostgreSqlContext : DbContext
{
    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<ProfessionalProfile> ProfessionalProfile { get; set; }
    public DbSet<Privacy> Privacy { get; set; }
    public DbSet<ProjectTask> ProjectTask { get; set; }
    public DbSet<ProjectIssue> ProjectIssue { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");

        builder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.HasIndex(user => user.Username).IsUnique();
            entity.HasIndex(user => user.Email).IsUnique();
        });
        base.OnModelCreating(builder);

        builder.Entity<ProfessionalProfile>(entity =>
        {
            entity.HasKey(profile => profile.UserId);
        });

        builder.Entity<Privacy>(entity =>
        {
            entity.HasKey(privacy => privacy.UserId);
        });

        builder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(task => task.Id);
        });

        builder.Entity<ProjectIssue>(entity =>
        {
            entity.HasKey(issue => issue.Id);
        });
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        return base.SaveChanges();
    }
}
