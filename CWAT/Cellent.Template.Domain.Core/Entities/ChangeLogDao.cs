using System.Data.Entity;

namespace Cellent.Template.Domain.Core.Entities
{
    public partial class ChangeLogDao
    {
        /// <summary>
        /// Gets or sets the domain object.
        /// </summary>
        /// <value>
        /// The domain object.
        /// </value>
        public virtual DomainObjectDao DomainObject { get; set; }

        static partial void OnCreateTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChangeLogDao>()
                .HasRequired(d => d.DomainObject);
        }
    }
}