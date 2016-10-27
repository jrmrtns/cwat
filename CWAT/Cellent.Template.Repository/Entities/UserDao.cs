using System;
using System.Data.Entity;

namespace Cellent.Template.Repository.Entities
{
    /// <summary>
    /// Repräsentiert das UserObjekt
    /// </summary>
    public partial class UserDao
    {
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public virtual RoleDao Role { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Initalizes Database
        /// </summary>
        /// <param name="modelBuilder"></param>
        static partial void OnCreateTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDao>()
                .HasRequired(d => d.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId);
        }
    }
}