using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SEC.UI.Dependency
{
    public class DependencyResolver
    {
        public void Install(IWindsorContainer container)
        {
            RegisterApplicationServices(container);
            RegisterUtilities(container);
        }

        private void RegisterApplicationServices(IWindsorContainer container)
        {
            container.Register(Classes.FromAssembly(Assembly.Load("SEC.Services"))
                .Pick()
                .WithService.InterfaceNamespace("SEC.Services.Interfaces"));
        }

        private void RegisterUtilities(IWindsorContainer container)
        {
            container.Register(Classes.FromAssembly(Assembly.Load("SEC.Utilities"))
                .Pick()
                .WithService.InterfaceNamespace("SEC.Utilities.Interfaces"));
        }
    }
}
