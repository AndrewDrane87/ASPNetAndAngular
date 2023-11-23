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

    public DbSet<Message> Messages { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<PlayerCharacter> PlayerCharacters { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemPhoto> ItemPhotoCollection { get; set; }
    public DbSet<Adventure> Adventures { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationLink> LocationLink { get; set; }
    public DbSet<NPC> NPCs { get; set; }
    public DbSet<DialogueNode> DialogueNodes { get; set; }
    public DbSet<DialogueLink> DialogueLinks { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Interaction> Interactions { get; set; }
    public DbSet<Trigger> Triggers { get; set; }
    public DbSet<Enemy> Enemies { get; set; }
    public DbSet<AdventureSave> AdventureSaves { get; set; }
    public DbSet<LocationSave> LocationSaves { get; set; }
    public DbSet<TriggerSave> TriggerSaves { get; set; }
    public DbSet<InteractionSave> InteractionSaves { get; set; }
    public DbSet<EnemySave> EnemySaves { get; set; }
    public DbSet<ContainerSave> ContainerSaves { get; set; }
    public DbSet<ItemSave> ItemSaves { get; set; }
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

        /*
        builder.Entity<DialogueResponseLink>()
            .HasOne(d => d.ChildDialogue)
            .WithMany(r => r.ParentResponses);
        */

        builder.Entity<DialogueLink>().HasKey(e => new { e.FromDialogueId, e.ToDialogueId });
        builder.Entity<DialogueLink>()
            .HasOne(link => link.FromDialogue)
            .WithMany(d => d.ToDialogueLinks)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
