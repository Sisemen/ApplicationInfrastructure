using CommonServiceLocator;
using Core.Kernel.DataAccess.Model;
using Core.Kernel.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Kernel.DataAccess.Context
{
    public class EfCoreContext : DbContext
    {
        private readonly IContextHelper _contextHelper = null!;
        private readonly ILogger<EfCoreContext> _logger;

        public EfCoreContext() : base()
        {
            _logger = ServiceLocator.Current.GetInstance<ILogger<EfCoreContext>>();

            base.Database.AutoTransactionBehavior = AutoTransactionBehavior.Always;
        }

        public EfCoreContext(DbContextOptions<EfCoreContext> options, ILogger<EfCoreContext> logger) : base(options)
        {
            _logger = logger;

            base.Database.AutoTransactionBehavior = AutoTransactionBehavior.Always;
        }

        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
            _logger = ServiceLocator.Current.GetInstance< ILogger<EfCoreContext>>();

            base.Database.AutoTransactionBehavior = AutoTransactionBehavior.Always;
        }

        public EfCoreContext(DbContextOptions<EfCoreContext> options, IContextHelper contextHelper, ILogger<EfCoreContext> logger)
            : base(options)
        {
            _logger = logger;
            _contextHelper = contextHelper;

            base.Database.AutoTransactionBehavior = AutoTransactionBehavior.Always;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assemblies = AssemblyHelper.FindAssemblies("*.Model.dll", string.Empty);

            foreach (var entityType in assemblies.SelectMany(assembly => assembly.GetTypes())
                                                 .Where(t => t.GetInterfaces().Any(i => i.Name == nameof(IEntity)) && t.IsClass && !t.IsAbstract && !t.ContainsGenericParameters))
            {
                var entity = Activator.CreateInstance(entityType) as IEntity;

                if (entity == null)
                {
                    _logger.LogWarning("Context.OnModelCreating: {entityFullName} entity was not found.", entityType.FullName);
                }

                entity?.OnConfiguringEntity(modelBuilder);
            }
        }

        public IQueryable<T> Query<T>(bool noTracking) where T : class, IEntity
        {
            return noTracking
                       ? base.Set<T>().AsQueryable().AsNoTracking()
                       : base.Set<T>().AsQueryable();
        }

        public async Task<T?> GetAsync<T>(object id) where T : class, IEntity => await base.Set<T>().FindAsync(id);

        public async Task<T> InsertAsync<T>(T entity) where T : class, IEntity
        {
            await base.Set<T>().AddAsync(entity);

            switch (entity)
            {
                case IntBasedEntity intBasedEntity:
                    intBasedEntity.TemporaryId = base.Entry(intBasedEntity).Property(p => p.Id).CurrentValue;
                    break;
                default:
                    _logger.LogError("Unsupported entity type. EntityName: {entityName}.", typeof(T).Name);

                    throw new Exception("Unsupported entity type.");
            }

            return entity;
        }

        public async Task InsertAsync<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            await base.Set<T>().AddRangeAsync(entities);
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : class, IEntity
        {
            object id;

            switch (entity)
            {
                case IntBasedEntity intBasedEntity:
                    id = base.Entry(intBasedEntity).Property(p => p.Id).CurrentValue;
                    break;
                default:
                    _logger.LogError($"Unsupported entity type. EntityName: {nameof(T)}.");
                    return false;
            }

            try
            {
                base.Entry(await base.Set<T>().FindAsync(id)).State = EntityState.Modified;

                return true;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                _logger.LogWarning(exception, "The entity does not exist in database to update.");

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                                 $"An exception has occurred while updating entity. EntityName: {nameof(T)}, Id: {id}.");

                return false;
            }
        }

        public void Update<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            var list = entities.ToList();

            list.ForEach(item => base.Entry(item).State = EntityState.Modified);

            base.Set<T>().UpdateRange(list);
        }

        public bool Delete<T>(object id) where T : class, IEntity
        {
            try
            {
                var existing = base.Set<T>().Find(id);

                if (existing == null)
                    return true;

                base.Set<T>().Remove(existing);

                return true;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                _logger.LogWarning(exception,
                                   "The entity does not exist in database to delete. EntityName: {entityName}, Id: {id}.",
                                   typeof(T).Name,
                                   id);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,
                                 "An exception has occurred while deleting entity. EntityName: {entityName}, Id: {id}.",
                                 typeof(T).Name,
                                 id);

                return false;
            }
        }

        public void Delete<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            base.Set<T>().RemoveRange(entities);
        }

        public async Task ApplyChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        public new void Dispose()
        {
            base.Database.CurrentTransaction?.Dispose();
            base.Dispose();
        }
    }
}
