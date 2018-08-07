using Autofac;
using Autofac.Integration.Mvc;
using AutoFacUntil.Attributes;
using AutoFacUntil.Enum;
using AutoFacUntil.Interface;
using System;
using System.Linq;
using System.Reflection;

namespace Fhey.Framework.Autofac
{
    public class AutofacUility
    {
        /// <summary>Assembly
        /// 调用者Assembly
        /// </summary>
        public Assembly CallingAssembly { get; set; }

        public AutofacUility()
        {
            CallingAssembly = Assembly.GetCallingAssembly();
        }

        /// <summary>
        /// 自动装配(特性模式)
        /// </summary>
        /// <returns></returns>
        public AutofacDependencyResolver AutoRegisterByAttribute()
        {
            return AutoRegister(RegisterType.Attribute);
        }

        /// <summary>
        /// 自动装配(接口模式)
        /// </summary>
        /// <returns></returns>
        public AutofacDependencyResolver AutoRegisterByInterface()
        {
            return AutoRegister(RegisterType.Interface);
        }

        /// <summary>
        /// 自动装配(特定字符串结尾模式)
        /// </summary>
        /// <returns></returns>
        public AutofacDependencyResolver AutoRegisterByEndsWith(string endsWith)
        {
            return AutoRegister(RegisterType.EndsWith, endsWith);
        }

        /// <summary>
        /// 自动装配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="endsWith"></param>
        /// <returns></returns>
        public AutofacDependencyResolver AutoRegister(RegisterType type,string endsWith = null)
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
                case RegisterType.EndsWith:
                    if (string.IsNullOrEmpty(endsWith))
                    {
                        return null;
                    }
                    types = types.Where(t => t.Name.EndsWith(endsWith));
                    break;
            }

            types.AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterControllers(CallingAssembly).PropertiesAutowired();
            return new AutofacDependencyResolver(builder.Build());
        }
    }
}
