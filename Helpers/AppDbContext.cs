using Microsoft.EntityFrameworkCore;
using TaskRecordingSystem.Models;

public class AppDbContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<ChecklistItem> ChecklistItems => Set<ChecklistItem>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Attachment> Attachments => Set<Attachment>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Reporter)
            .WithMany(u => u.ReportedTasks)
            .HasForeignKey(t => t.ReporterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Assignee)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssigneeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}