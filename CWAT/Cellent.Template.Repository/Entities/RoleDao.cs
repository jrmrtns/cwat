using System.Collections.Generic;
using System.Data.Entity;

namespace Cellent.Template.Repository.Entities
{
    public partial class RoleDao
    {
        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>
        /// The rights.
        /// </value>
        public virtual ICollection<RightDao> Rights { get; set; }

        /// <summary>
        /// Initalizes Database
        /// </summary>
        /// <param name="modelBuilder"></param>
        static partial void OnCreateTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleDao>()
                        .HasMany(d => d.Rights)
                        .WithMany()
                        .Map(d => d.ToTable("RoleRightMap"));
        }
    }
}