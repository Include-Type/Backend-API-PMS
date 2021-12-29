namespace IncludeTypeBackend.Services;

public class PostgreSqlContext : DbContext
{
    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<UserVerification> UserVerification { get; set; }
    public DbSet<ProfessionalProfile> ProfessionalProfile { get; set; }
    public DbSet<Privacy> Privacy { get; set; }
    public DbSet<ProjectTask> ProjectTask { get; set; }
    public DbSet<ProjectIssue> ProjectIssue { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<ProjectMember> ProjectMember { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("public");

        builder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.HasIndex(user => user.Username).IsUnique();
            entity.HasIndex(user => user.Email).IsUnique();
        });

        builder.Entity<UserVerification>(entity =>
        {
            entity.HasKey(verifier => verifier.UniqueString);
        });

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

        builder.Entity<Project>(entity =>
        {
            entity.HasKey(project => project.Id);
            entity.HasIndex(project => project.Name).IsUnique();
        });

        builder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(member => member.Id);
        });

        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        return base.SaveChanges();
    }
}
