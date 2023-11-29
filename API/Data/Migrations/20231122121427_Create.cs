using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnownAs = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastActive = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DialogueNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueNodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ItemPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: true),
                    PublicId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    SenderUsername = table.Column<string>(type: "text", nullable: true),
                    RecipientId = table.Column<int>(type: "integer", nullable: false),
                    RecipientUsername = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    DateRead = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MessageSent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SenderDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    RecipientDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialogueLinks",
                columns: table => new
                {
                    FromDialogueId = table.Column<int>(type: "integer", nullable: false),
                    ToDialogueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueLinks", x => new { x.FromDialogueId, x.ToDialogueId });
                    table.ForeignKey(
                        name: "FK_DialogueLinks_DialogueNodes_FromDialogueId",
                        column: x => x.FromDialogueId,
                        principalTable: "DialogueNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DialogueLinks_DialogueNodes_ToDialogueId",
                        column: x => x.ToDialogueId,
                        principalTable: "DialogueNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_Connections_Groups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "Groups",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RequiredLevel = table.Column<int>(type: "integer", nullable: false),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    AttackValue = table.Column<int>(type: "integer", nullable: false),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    DamageModifiers = table.Column<string>(type: "text", nullable: true),
                    StatModifiers = table.Column<string>(type: "text", nullable: true),
                    ResistanceModifiers = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: true),
                    DamageType = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    StackSize = table.Column<int>(type: "integer", nullable: false),
                    Use = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "ItemPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Enemies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    MaxHp = table.Column<int>(type: "integer", nullable: false),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    MovementRange = table.Column<int>(type: "integer", nullable: false),
                    AttackStrategy = table.Column<string>(type: "text", nullable: true),
                    Attack1Name = table.Column<string>(type: "text", nullable: true),
                    Attack1Range = table.Column<int>(type: "integer", nullable: false),
                    Attack1BaseDamage = table.Column<int>(type: "integer", nullable: false),
                    Attack2Name = table.Column<string>(type: "text", nullable: true),
                    Attack2Range = table.Column<int>(type: "integer", nullable: false),
                    Attack2BaseDamage = table.Column<int>(type: "integer", nullable: false),
                    ModifierDiceSides = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enemies_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActionTriggerSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionTriggerId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Complete = table.Column<bool>(type: "boolean", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTriggerSaves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adventures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartingLocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adventures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdventureVariable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    InitialValue = table.Column<string>(type: "text", nullable: true),
                    AdventureId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureVariable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureVariable_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ShortDescription = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    VisibilityRequirements = table.Column<string>(type: "text", nullable: true),
                    RoomNumber = table.Column<int>(type: "integer", nullable: true),
                    AdventureId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Containers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EnemyLocationLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    EnemyId = table.Column<int>(type: "integer", nullable: false),
                    RequiredPlayerCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemyLocationLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnemyLocationLink_Enemies_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnemyLocationLink_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Information = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationLink",
                columns: table => new
                {
                    FromId = table.Column<int>(type: "integer", nullable: false),
                    ToId = table.Column<int>(type: "integer", nullable: false),
                    FromLocationId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationLink", x => new { x.FromId, x.ToId });
                    table.ForeignKey(
                        name: "FK_LocationLink_Locations_FromLocationId",
                        column: x => x.FromLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationLink_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NPCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    DialogueId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPCs_DialogueNodes_DialogueId",
                        column: x => x.DialogueId,
                        principalTable: "DialogueNodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPCs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemContainerLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    ContainerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemContainerLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemContainerLink_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemContainerLink_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventType = table.Column<string>(type: "text", nullable: true),
                    ActionType = table.Column<string>(type: "text", nullable: true),
                    ActionData = table.Column<string>(type: "text", nullable: true),
                    ResultData = table.Column<string>(type: "text", nullable: true),
                    ContainerId = table.Column<int>(type: "integer", nullable: true),
                    DialogueNodeId = table.Column<int>(type: "integer", nullable: true),
                    InteractionId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triggers_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Triggers_DialogueNodes_DialogueNodeId",
                        column: x => x.DialogueNodeId,
                        principalTable: "DialogueNodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Triggers_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Triggers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdventureSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaveDescription = table.Column<string>(type: "text", nullable: true),
                    AdventureId = table.Column<int>(type: "integer", nullable: false),
                    CurrentLocationId = table.Column<int>(type: "integer", nullable: true),
                    AppUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureSaves_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureSaves_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdventureVariableSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdventureVariableId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    AdventureSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureVariableSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureVariableSave_AdventureSaves_AdventureSaveId",
                        column: x => x.AdventureSaveId,
                        principalTable: "AdventureSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdventureVariableSave_AdventureVariable_AdventureVariableId",
                        column: x => x.AdventureVariableId,
                        principalTable: "AdventureVariable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdventureSaveId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    IsCurrentLocation = table.Column<bool>(type: "boolean", nullable: false),
                    VisibilityRequirement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationSaves_AdventureSaves_AdventureSaveId",
                        column: x => x.AdventureSaveId,
                        principalTable: "AdventureSaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationSaves_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContainerSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Complete = table.Column<bool>(type: "boolean", nullable: false),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    LocationSaveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerSaves_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerSaves_LocationSaves_LocationSaveId",
                        column: x => x.LocationSaveId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnemySaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnemyLocationLinkId = table.Column<int>(type: "integer", nullable: false),
                    EnemyId = table.Column<int>(type: "integer", nullable: false),
                    CurrentHp = table.Column<int>(type: "integer", nullable: false),
                    LocationSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemySaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnemySaves_Enemies_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnemySaves_EnemyLocationLink_EnemyLocationLinkId",
                        column: x => x.EnemyLocationLinkId,
                        principalTable: "EnemyLocationLink",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnemySaves_LocationSaves_LocationSaveId",
                        column: x => x.LocationSaveId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerSaveId = table.Column<int>(type: "integer", nullable: true),
                    LocationSaveId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    StorageIndex = table.Column<int>(type: "integer", nullable: false),
                    CurrentStackSize = table.Column<int>(type: "integer", nullable: false),
                    PlayerCharacterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                        column: x => x.ContainerSaveId,
                        principalTable: "ContainerSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemSaves_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                        column: x => x.LocationSaveId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerCharacters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    HelmetItemSaveId = table.Column<int>(type: "integer", nullable: true),
                    LeftHandItemSaveId = table.Column<int>(type: "integer", nullable: true),
                    RightHandItemSaveId = table.Column<int>(type: "integer", nullable: true),
                    BodyItemSaveId = table.Column<int>(type: "integer", nullable: true),
                    FeetItemSaveId = table.Column<int>(type: "integer", nullable: true),
                    AdventureSaveId = table.Column<int>(type: "integer", nullable: true),
                    AppUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_AdventureSaves_AdventureSaveId",
                        column: x => x.AdventureSaveId,
                        principalTable: "AdventureSaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_ItemSaves_BodyItemSaveId",
                        column: x => x.BodyItemSaveId,
                        principalTable: "ItemSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_ItemSaves_FeetItemSaveId",
                        column: x => x.FeetItemSaveId,
                        principalTable: "ItemSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_ItemSaves_HelmetItemSaveId",
                        column: x => x.HelmetItemSaveId,
                        principalTable: "ItemSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_ItemSaves_LeftHandItemSaveId",
                        column: x => x.LeftHandItemSaveId,
                        principalTable: "ItemSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerCharacters_ItemSaves_RightHandItemSaveId",
                        column: x => x.RightHandItemSaveId,
                        principalTable: "ItemSaves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionTriggerSaves_ActionTriggerId",
                table: "ActionTriggerSaves",
                column: "ActionTriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionTriggerSaves_LocationId",
                table: "ActionTriggerSaves",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_StartingLocationId",
                table: "Adventures",
                column: "StartingLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureSaves_AdventureId",
                table: "AdventureSaves",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureSaves_AppUserId",
                table: "AdventureSaves",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureSaves_CurrentLocationId",
                table: "AdventureSaves",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureVariable_AdventureId",
                table: "AdventureVariable",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureVariableSave_AdventureSaveId",
                table: "AdventureVariableSave",
                column: "AdventureSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureVariableSave_AdventureVariableId",
                table: "AdventureVariableSave",
                column: "AdventureVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Connections_GroupName",
                table: "Connections",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_LocationId",
                table: "Containers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerSaves_ContainerId",
                table: "ContainerSaves",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerSaves_LocationSaveId",
                table: "ContainerSaves",
                column: "LocationSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueLinks_ToDialogueId",
                table: "DialogueLinks",
                column: "ToDialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_Enemies_PhotoId",
                table: "Enemies",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyLocationLink_EnemyId",
                table: "EnemyLocationLink",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyLocationLink_LocationId",
                table: "EnemyLocationLink",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemySaves_EnemyId",
                table: "EnemySaves",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemySaves_EnemyLocationLinkId",
                table: "EnemySaves",
                column: "EnemyLocationLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemySaves_LocationSaveId",
                table: "EnemySaves",
                column: "LocationSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_LocationId",
                table: "Interactions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemContainerLink_ContainerId",
                table: "ItemContainerLink",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemContainerLink_ItemId",
                table: "ItemContainerLink",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PhotoId",
                table: "Items",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaves_ContainerSaveId",
                table: "ItemSaves",
                column: "ContainerSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaves_ItemId",
                table: "ItemSaves",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaves_LocationSaveId",
                table: "ItemSaves",
                column: "LocationSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaves_PlayerCharacterId",
                table: "ItemSaves",
                column: "PlayerCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationLink_FromLocationId",
                table: "LocationLink",
                column: "FromLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationLink_LocationId",
                table: "LocationLink",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AdventureId",
                table: "Locations",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationSaves_AdventureSaveId",
                table: "LocationSaves",
                column: "AdventureSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationSaves_LocationId",
                table: "LocationSaves",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCs_DialogueId",
                table: "NPCs",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCs_LocationId",
                table: "NPCs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_AdventureSaveId",
                table: "PlayerCharacters",
                column: "AdventureSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_AppUserId",
                table: "PlayerCharacters",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_BodyItemSaveId",
                table: "PlayerCharacters",
                column: "BodyItemSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_FeetItemSaveId",
                table: "PlayerCharacters",
                column: "FeetItemSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_HelmetItemSaveId",
                table: "PlayerCharacters",
                column: "HelmetItemSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_LeftHandItemSaveId",
                table: "PlayerCharacters",
                column: "LeftHandItemSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCharacters_RightHandItemSaveId",
                table: "PlayerCharacters",
                column: "RightHandItemSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_ContainerId",
                table: "Triggers",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_DialogueNodeId",
                table: "Triggers",
                column: "DialogueNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_InteractionId",
                table: "Triggers",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_LocationId",
                table: "Triggers",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTriggerSaves_LocationSaves_LocationId",
                table: "ActionTriggerSaves",
                column: "LocationId",
                principalTable: "LocationSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTriggerSaves_Triggers_ActionTriggerId",
                table: "ActionTriggerSaves",
                column: "ActionTriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_Locations_StartingLocationId",
                table: "Adventures",
                column: "StartingLocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdventureSaves_LocationSaves_CurrentLocationId",
                table: "AdventureSaves",
                column: "CurrentLocationId",
                principalTable: "LocationSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_PlayerCharacters_PlayerCharacterId",
                table: "ItemSaves",
                column: "PlayerCharacterId",
                principalTable: "PlayerCharacters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventureSaves_LocationSaves_CurrentLocationId",
                table: "AdventureSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerSaves_LocationSaves_LocationSaveId",
                table: "ContainerSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_Locations_StartingLocationId",
                table: "Adventures");

            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Locations_LocationId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdventureSaves_Adventures_AdventureId",
                table: "AdventureSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_AdventureSaves_AspNetUsers_AppUserId",
                table: "AdventureSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_AspNetUsers_AppUserId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_AdventureSaves_AdventureSaveId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerSaves_Containers_ContainerId",
                table: "ContainerSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_Items_ItemId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_PlayerCharacters_PlayerCharacterId",
                table: "ItemSaves");

            migrationBuilder.DropTable(
                name: "ActionTriggerSaves");

            migrationBuilder.DropTable(
                name: "AdventureVariableSave");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "DialogueLinks");

            migrationBuilder.DropTable(
                name: "EnemySaves");

            migrationBuilder.DropTable(
                name: "ItemContainerLink");

            migrationBuilder.DropTable(
                name: "LocationLink");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "NPCs");

            migrationBuilder.DropTable(
                name: "Triggers");

            migrationBuilder.DropTable(
                name: "AdventureVariable");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "EnemyLocationLink");

            migrationBuilder.DropTable(
                name: "DialogueNodes");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Enemies");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "LocationSaves");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Adventures");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AdventureSaves");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemPhotos");

            migrationBuilder.DropTable(
                name: "ContainerSaves");

            migrationBuilder.DropTable(
                name: "PlayerCharacters");

            migrationBuilder.DropTable(
                name: "ItemSaves");
        }
    }
}
