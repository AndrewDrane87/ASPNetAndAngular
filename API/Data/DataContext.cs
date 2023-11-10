using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : IdentityDbContext<AppUser, AppRole, int,
    IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserLike> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<PlayerCharacter> PlayerCharacters { get; set; }
    public DbSet<Item> ItemCollection { get; set; }
    public DbSet<ItemPhoto> ItemPhotoCollection { get; set; }
    public DbSet<Adventure> Adventures { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationLink> LocationLink { get; set; }
    public DbSet<NPC> NPCCollection { get; set; }
    public DbSet<Dialogue> DialogueCollection { get; set; }
    public DbSet<DialogueResponse> ResponseCollection { get; set; }
    public DbSet<Container> ContainerCollection { get; set; }
    public DbSet<Interaction> Interactions { get; set; }
    public DbSet<ActionTrigger> Triggers { get; set; }
    public DbSet<Enemy> EnemyCollection { get; set; }
    public DbSet<AdventureSave> AdventureSaves { get; set; }
    public DbSet<LocationSave> LocationSaves { get; set; }
    public DbSet<ActionTriggerSave> ActionTriggerSaves { get; set; }
    public DbSet<EnemySave> EnemySaves { get; set; }
    public DbSet<ContainerSave> ContainerSaves { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<AppUser>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(u => u.User)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();

        builder.Entity<AppRole>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(u => u.Role)
        .HasForeignKey(ur => ur.RoleId)
        .IsRequired();

        builder.Entity<UserLike>()
        .HasKey(k => new { k.SourceUserId, k.TargetUserId });

        builder.Entity<UserLike>()
        .HasOne(s => s.SourceUser)
        .WithMany(l => l.LikedUsers)
        .HasForeignKey(s => s.SourceUserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserLike>()
        .HasOne(s => s.TargetUser)
        .WithMany(l => l.LikedByUsers)
        .HasForeignKey(s => s.TargetUserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Message>()
        .HasOne(u => u.Recipient)
        .WithMany(m => m.MessagesReceived)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Message>()
        .HasOne(u => u.Sender)
        .WithMany(m => m.MessagesSent)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<LocationLink>().HasKey(e => new { e.FromId, e.ToId });
        builder.Entity<LocationLink>()
            .HasOne(e => e.FromLocation)
            .WithMany(l => l.ConnectedToLocations)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<AdventureSave>()
            .HasMany(a => a.LocationSaves)
            .WithOne(l => l.AdventureSave)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<AdventureSave>()
            .HasMany(a => a.PlayerCharacters)
            .WithOne(pc => pc.AdventureSave)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LocationSave>()
            .HasMany(l => l.Triggers)
            .WithOne(t => t.LocationSave)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LocationSave>()
            .HasMany(l => l.Containers)
            .WithOne(c => c.LocationSave)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
