using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SEC.UI.Dependency
{
    public static class WindsorExtensions
    {       

        public static BasedOnDescriptor InterfaceNamespace(this ServiceDescriptor descriptor, string interfaceNamespace)
        {
            return descriptor.Select(delegate (Type type, Type[] baseType)
            {
                var interfaces = type.GetInterfaces()
                    .Where(t => !t.IsGenericType
                                && (t.Namespace != null && t.Namespace.StartsWith(interfaceNamespace)));

                var enumerable = interfaces as IList<Type> ?? interfaces.ToList();
                if (enumerable.Any())
                {
                    return new[] { enumerable.ElementAt(0) };
                }

                return null;
            });
        }
    }
}
