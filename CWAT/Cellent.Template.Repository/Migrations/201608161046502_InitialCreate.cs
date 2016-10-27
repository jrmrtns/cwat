namespace Cellent.Template.Repository.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Migration
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class InitialCreate : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeLogs",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    ObjectId = c.Guid(),
                    OldValue = c.String(maxLength: 200),
                    NewValue = c.String(maxLength: 200),
                    Property = c.String(nullable: false, maxLength: 200),
                    CreatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    ChangedAt = c.DateTime(nullable: false),
                    ChangedBy = c.Guid(nullable: false),
                    DomainObject_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DomainObjects", t => t.DomainObject_Id, cascadeDelete: true)
                .Index(t => t.DomainObject_Id);

            CreateTable(
                "dbo.DomainObjects",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Type = c.String(nullable: false, maxLength: 200),
                    Assembly = c.String(nullable: false, maxLength: 100),
                    EntityType = c.String(maxLength: 200),
                    EntityAssembly = c.String(nullable: false, maxLength: 100),
                    DisplayName = c.String(maxLength: 100),
                    CreatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    ChangedAt = c.DateTime(nullable: false),
                    ChangedBy = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Resources",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Key = c.String(nullable: false, maxLength: 30),
                    Language = c.String(nullable: false, maxLength: 5),
                    Description = c.String(maxLength: 255),
                    Translation = c.String(maxLength: 100),
                    CreatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    ChangedAt = c.DateTime(nullable: false),
                    ChangedBy = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Rights",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 100),
                    Claim = c.String(nullable: false, maxLength: 100),
                    Resource = c.String(nullable: false, maxLength: 100),
                    CreatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    ChangedAt = c.DateTime(nullable: false),
                    ChangedBy = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 200),
                    CreatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    ChangedAt = c.DateTime(nullable: false),
                    ChangedBy = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 100),
                    Firstname = c.String(nullable: false, maxLength: 100),
                    Lastname = c.String(nullable: false, maxLength: 100),
                    Company = c.String(maxLength: 50),
                    Department = c.String(maxLength: 50),
                    Workplace = c.String(maxLength: 100),
                    Manager = c.String(maxLength: 100),
                    Email = c.String(maxLength: 100),
                    Phone = c.String(maxLength: 50),
                    Mobile = c.String(maxLength: 50),
                    Fax = c.String(maxLength: 50),
                    LastLogin = c.DateTime(),
                    Deactivated = c.Boolean(nullable: false),
                    RoleId = c.Guid(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Guid(nullable: false),
                    ChangedAt = c.DateTime(nullable: false),
                    ChangedBy = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.RoleRightMap",
                c => new
                {
                    RoleDao_Id = c.Guid(nullable: false),
                    RightDao_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.RoleDao_Id, t.RightDao_Id })
                .ForeignKey("dbo.Roles", t => t.RoleDao_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rights", t => t.RightDao_Id, cascadeDelete: true)
                .Index(t => t.RoleDao_Id)
                .Index(t => t.RightDao_Id);
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleRightMap", "RightDao_Id", "dbo.Rights");
            DropForeignKey("dbo.RoleRightMap", "RoleDao_Id", "dbo.Roles");
            DropForeignKey("dbo.ChangeLogs", "DomainObject_Id", "dbo.DomainObjects");
            DropIndex("dbo.RoleRightMap", new[] { "RightDao_Id" });
            DropIndex("dbo.RoleRightMap", new[] { "RoleDao_Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.ChangeLogs", new[] { "DomainObject_Id" });
            DropTable("dbo.RoleRightMap");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Rights");
            DropTable("dbo.Resources");
            DropTable("dbo.DomainObjects");
            DropTable("dbo.ChangeLogs");
        }
    }
}