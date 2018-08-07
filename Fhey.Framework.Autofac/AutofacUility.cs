using Autofac;
using Autofac.Integration.Mvc;
using AutoFacUntil.Attributes;
using AutoFacUntil.Enum;
using AutoFacUntil.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Autofac
{
    public class AutofacUility
    {
        public AutofacUility()
        {
            CallingAssembly = Assembly.GetCallingAssembly();
        }

        public Assembly CallingAssembly { get; set; }

        public AutofacDependencyResolver AutoRegisterByAttribute()
        {
            return AutoRegister(RegisterType.Attribute);
        }

        public AutofacDependencyResolver AutoRegisterByInterface()
        {
            return AutoRegister(RegisterType.Interface);
        }

        public AutofacDependencyResolver AutoRegister(RegisterType type)
        {
            var builder = new ContainerBuilder();
            var types = builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies());
            switch (type)
            {
                case RegisterType.Attribute:
                    types = types.Where(t => t.GetCustomAttribute<AutoWriteAttribute>() != null);
                    break;
                case RegisterType.Interface:
                    var baseType = typeof(IAutoWrite);
                    types = types.Where(t => baseType.IsAssignableFrom(t) && t != baseType);
                    break;
            }

            types.AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterControllers(CallingAssembly).PropertiesAutowired();
            return new AutofacDependencyResolver(builder.Build());
        }
    }
}
