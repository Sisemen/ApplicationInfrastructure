using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core.Kernel.Helper
{
    public static class AssemblyHelper
    {
        public static IList<Assembly> FindAssemblies(string includeCondition, string excludeCondition)
        {
            var executionDirectory = AppContext.BaseDirectory
                ?? throw new Exception("Can't find Execution Directory!!!");
            var excludedAssemblies = string.IsNullOrEmpty(excludeCondition)
                                         ? new List<string>()
                                         : Directory.GetFiles(executionDirectory, excludeCondition)
                                                    .ToList();
            return Directory.GetFiles(executionDirectory, includeCondition)
                            .Where(assembly => !excludedAssemblies.Contains(assembly))
                            .Select(Assembly.LoadFile)
                            .Select(assembly => Assembly.Load(assembly.FullName))
                            .ToList();
        }
    }
}