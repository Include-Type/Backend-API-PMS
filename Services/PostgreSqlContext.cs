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
            entity.HasData(
                new User
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    Password = HashPassword("1234567890"),
                    FirstName = "Subham",
                    LastName = "Karmakar",
                    Username = "SubhamK108",
                    Email = "subhamkarmakar0901@gmail.com",
                    IsAdmin = true
                });
        });

        builder.Entity<UserVerification>(entity =>
        {
            entity.HasKey(verifier => verifier.UniqueString);
            //entity
            //    .HasOne<User>(uv => uv.User)
            //    .WithOne(u => u.UserVerification)
            //    .HasForeignKey<UserVerification>(uv => uv.UserId);
        });

        builder.Entity<ProfessionalProfile>(entity =>
        {
            entity.HasKey(profile => profile.UserId);
            //entity
            //    .HasOne<User>(pp => pp.User)
            //    .WithOne(u => u.ProfessionalProfile)
            //    .HasForeignKey<ProfessionalProfile>(pp => pp.UserId);
        });

        builder.Entity<Privacy>(entity =>
        {
            entity.HasKey(privacy => privacy.UserId);
            //entity
            //    .HasOne<User>(pr => pr.User)
            //    .WithOne(u => u.Privacy)
            //    .HasForeignKey<Privacy>(pr => pr.UserId);
        });

        builder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(task => task.Id);
            //entity
            //    .HasOne<Project>(pt => pt.Project)
            //    .WithMany(p => p.ProjectTasks)
            //    .HasForeignKey(pt => pt.ProjId);
        });

        builder.Entity<ProjectIssue>(entity =>
        {
            entity.HasKey(issue => issue.Id);
            //entity
            //    .HasOne<Project>(pi => pi.Project)
            //    .WithMany(p => p.ProjectIssues)
            //    .HasForeignKey(pi => pi.ProjId);
        });

        builder.Entity<Project>(entity =>
        {
            entity.HasKey(project => project.Id);
            entity.HasIndex(project => project.Name).IsUnique();
        });

        builder.Entity<ProjectMember>(entity =>
        {
            entity.HasKey(member => member.Id);
            //entity
            //    .HasOne<Project>(pm => pm.Project)
            //    .WithMany(p => p.ProjectMembers)
            //    .HasForeignKey(pm => pm.ProjName);
            //entity
            //    .HasOne<User>(pm => pm.User)
            //    .WithOne(p => p.ProjectMember)
            //    .HasForeignKey<ProjectMember>(pm => pm.Id);
        });

        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        return base.SaveChanges();
    }
}
