using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Application.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Assembly> GetProjectReferencedAssemblies(this Assembly assembly)
        {
            return assembly.GetReferencedAssemblies()
                .Where(a =>
                !a.FullName.StartsWith("System") && 
                !a.FullName.StartsWith("Microsoft") &&
                !a.FullName.StartsWith("MassTransit"))
                .Select(Assembly.Load).Distinct();
        }
    }
}
