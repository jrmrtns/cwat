using Cellent.Template.Domain.Core;
using Cellent.Template.Repository.Entities;
using System.Data.Entity;

namespace Cellent.Template.Repository.Context
{
    /// <summary>
    /// Der Context für EF
    /// </summary>
    public class CellentContext : BaseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellentContext"/> class.
        /// </summary>
        public CellentContext()
            : base("name=Cellent")
        { }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<UserDao> Users { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public DbSet<RoleDao> Roles { get; set; }

        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>
        /// The rights.
        /// </value>
        public DbSet<RightDao> Rights { get; set; }

        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        /// <value>
        /// The resources.
        /// </value>
        public DbSet<ResourceDao> Resources { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            UserDao.CreateTable(modelBuilder);
            RoleDao.CreateTable(modelBuilder);
            RightDao.CreateTable(modelBuilder);
            ResourceDao.CreateTable(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}