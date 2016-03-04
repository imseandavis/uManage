using System;
using System.Reflection;
using Ninject;
using Ninject.Extensions.Conventions;

namespace S203.uManage
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind(x =>
            {
                x.FromAssembliesMatching("*uManage*")
                    .SelectAllClasses()
                    .BindAllInterfaces();
            });

            return kernel;
        });
    }
}
