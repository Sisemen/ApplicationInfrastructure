using Core.Kernel.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Core.Kernel.Helper
{
    public class ContextHelper : IContextHelper
    {
        private readonly ILogger<ContextHelper> _logger;

        public ContextHelper(ILogger<ContextHelper> logger)
        {
            _logger = logger;
        }

        public void ApplyModelCreating(ModelBuilder modelBuilder)
        {
            var assemblies = AssemblyHelper.FindAssemblies("*.ModelLayer.dll", string.Empty);

            foreach (var entityType in assemblies.SelectMany(assembly => assembly.GetTypes())
                                                 .Where(t => t.GetInterfaces().Any(i => i.Name == nameof(IEntity)) && t.IsClass && !t.IsAbstract && !t.ContainsGenericParameters))
            {
                var entity = Activator.CreateInstance(entityType) as IEntity;

                if (entity == null)
                {
                    _logger.LogWarning("EfCoreContext.OnModelCreating: {entityFullName} entity was not found.", entityType.FullName);
                }

                entity?.OnConfiguringEntity(modelBuilder);
            }
        }
    }
}
