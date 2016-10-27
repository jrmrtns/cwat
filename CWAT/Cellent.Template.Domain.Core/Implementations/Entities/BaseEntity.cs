using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using Cellent.Template.Common.Interfaces.Core;
using Cellent.Template.Common.Validation;
using Cellent.Template.Domain.Core.Interfaces.Repositories;
using Microsoft.Practices.Unity;
using Prism.Events;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.Core.Implementations.Entities
{
    /// <summary>
    /// Basisklasse für DomainEntities
    /// </summary>
    public class BaseEntity<T> : IBaseEntity
    {
        private readonly IUnityContainer _container;
        private readonly IBaseRepository<T> _repository;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity{T}" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BaseEntity(IUnityContainer container, IBaseRepository<T> repository, IEventAggregator eventAggregator)
        {
            _container = container;
            _repository = repository;
            _eventAggregator = eventAggregator;
            State = Constants.EntityState.Created;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the changed at.
        /// </summary>
        /// <value>
        /// The changed at.
        /// </value>
        public DateTime ChangedAt { get; set; }

        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>
        /// The changed by.
        /// </value>
        public Guid ChangedBy { get; set; }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public IBaseRepository<T> Repository
        {
            get { return _repository; }
        }

        /// <summary>
        /// Saves the or update the entity asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual Task<T> SaveOrUpdateAsync(T entity)
        {
            IValidatable validatable = entity as IValidatable;
            if (validatable != null)
            {
                ValidationSummary validationSummary = validatable.Validate();
                if (!validationSummary.IsValid)
                    throw new ValidationException(validationSummary);
            }

            return Repository.SaveOrUpdateAsync(entity);
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public Constants.EntityState State { get; set; }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public IEventAggregator EventAggregator
        {
            get { return _eventAggregator; }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IUnityContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <param name="source">The source.</param>
        public void MapBaseFields(IBaseDao source)
        {
            Id = source.Id;
            CreatedAt = source.CreatedAt;
            CreatedBy = source.CreatedBy;
            ChangedAt = source.ChangedAt;
            ChangedBy = source.ChangedBy;
            State = source.State;
        }

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <param name="source">The source.</param>
        public void MapBaseFields(BaseDto source)
        {
            Id = source.Id;
            CreatedAt = source.CreatedAt;
            CreatedBy = source.CreatedBy;
            ChangedAt = source.ChangedAt;
            ChangedBy = source.ChangedBy;

            // ReSharper disable once SuspiciousTypeConversion.Global
            IUnitOfWork unitOfWork = this as IUnitOfWork;
            if (unitOfWork != null)
                unitOfWork.ChangeLog = source.ChangeLog;

            State = source.State;
        }

        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="md5Hash">The MD5 hash.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        protected static string GetMd5Hash(HashAlgorithm md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            foreach (byte b in data)
            {
                sBuilder.Append(b.ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}