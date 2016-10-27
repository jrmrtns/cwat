using Cellent.Template.Common.Constants;
using Cellent.Template.Domain.Core.Entities;
using Cellent.Template.Repository.Context;
using Cellent.Template.Repository.Entities;
using System;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;

namespace Cellent.Template.Repository.Migrations
{
    /// <summary>
    /// Configuration
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<CellentContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Cellent.Template.Repository.Context.CellentContext";
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(CellentContext context)
        {
            Guid id = Guid.Parse("8D8FEF52-E083-E511-827F-D8FC93B51552");

            context.SaveChanges();

            RoleDao adminRoleDao =
                new RoleDao
                {
                    Id = Constants.AdminGuid,
                    Name = "Administrator",
                    Description = "Vollzugriff",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                };

            context.Roles.AddOrUpdate(adminRoleDao);

            RoleDao guestRole =
                new RoleDao
                {
                    Id = Constants.GuestGuid,
                    Name = "Gast",
                    Description = "Lesender Zugriff",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                };

            context.Roles.AddOrUpdate(guestRole);

            context.Users.AddOrUpdate(
                new UserDao
                {
                    Id = id,
                    Name = "$userdomain$\\$username$",
                    Firstname = "$username$",
                    Lastname = "$username$",
                    Email = "me.me@cellent.de",
                    RoleId = Constants.AdminGuid,
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id,
                    Deactivated = false
                });

            context.DomainObjects.AddOrUpdate(
                new DomainObjectDao
                {
                    Id = Guid.Parse("{D6CEDDFC-5691-4D89-A801-0CC988E32165}"),
                    DisplayName = "User",
                    Assembly = "Cellent.Template.Domain.Core.Interfaces.Entities",
                    Type = "Cellent.Template.Domain.Core.Interfaces.Entities.IUser",
                    EntityAssembly = "Cellent.Template.Repository",
                    EntityType = "Cellent.Template.Repository.Entities.UserDao",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                },

                new DomainObjectDao
                {
                    Id = Guid.Parse("{06BE5038-5DD0-4C8E-873C-3630F299F47B}"),
                    DisplayName = "Role",
                    Assembly = "Cellent.Template.Domain.Core.Interfaces.Entities",
                    Type = "Cellent.Template.Domain.Core.Interfaces.Entities.IRole",
                    EntityAssembly = "Cellent.Template.Repository",
                    EntityType = "Cellent.Template.Repository.Entities.RoleDao",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                },

                new DomainObjectDao
                {
                    Id = Guid.Parse("{06800369-55A9-48BD-BE2C-7024B6572F17}"),
                    DisplayName = "Right",
                    Assembly = "Cellent.Template.Domain.Core.Interfaces.Entities",
                    Type = "Cellent.Template.Domain.Core.Interfaces.Entities.IRight",
                    EntityAssembly = "Cellent.Template.Repository",
                    EntityType = "Cellent.Template.Repository.Entities.RightDao",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                },

                new DomainObjectDao
                {
                    Id = Guid.Parse("{DBB7BAA3-042B-457C-A9AE-4A1500A6B02D}"),
                    DisplayName = "ChangeLog",
                    Assembly = "Cellent.Template.Domain.Core.Interfaces.Entities",
                    Type = "Cellent.Template.Domain.Core.Interfaces.Entities.IChangeLog",
                    EntityAssembly = "Cellent.Template.Repository",
                    EntityType = "Cellent.Template.Repository.Entities.ChangeLogDao",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                },

                new DomainObjectDao
                {
                    Id = Guid.Parse("{1C045758-53FB-4827-AD6B-7F387DC67C6E}"),
                    DisplayName = "GlobalSetting",
                    Assembly = "Cellent.Template.Domain.Core.Interfaces.Entities",
                    Type = "Cellent.Template.Domain.Core.Interfaces.Entities.IGlobalSetting",
                    EntityAssembly = "Cellent.Template.Repository",
                    EntityType = "Cellent.Template.Repository.Entities.GlobalSettingDao",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                },

                new DomainObjectDao
                {
                    Id = Guid.Parse("{2D3BBBAF-CAB5-431F-A36E-EA0BD3EE58C2}"),
                    DisplayName = "Resource",
                    Assembly = "Cellent.Template.Domain.Core.Interfaces.Entities",
                    Type = "Cellent.Template.Domain.Core.Interfaces.Entities.IResource",
                    EntityAssembly = "Cellent.Template.Repository",
                    EntityType = "Cellent.Template.Repository.Entities.ResourceDao",
                    ChangedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    ChangedBy = id,
                    CreatedBy = id
                });

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            Debug.WriteLine(baseDir);
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + "..\\..\\..\\Database\\Resources.sql"));
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + "..\\..\\..\\Database\\Rights.sql"));

            context.SaveChanges();

            context.Database.ExecuteSqlCommand("INSERT[dbo].[RoleRightMap]([RoleDao_Id], [RightDao_Id]) select N'" + Constants.GuestGuid + "', id from[dbo].[Rights] where Claim = 'http://www.daimler.com/Cellent/claims/users/allowedOperations'");
            context.Database.ExecuteSqlCommand("INSERT[dbo].[RoleRightMap]([RoleDao_Id], [RightDao_Id]) select N'" + Constants.AdminGuid + "', id from[dbo].[Rights]");
        }
    }
}