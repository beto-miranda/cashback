using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CashBack.Application.Common.Mapping
{
    public static class MapperProfileHelper
    {
        public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                    from type in types
                    from instance in type.GetInterfaces()
                    let i = instance
                    where
                        instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select new Map
                    {
                        Source = i.GetGenericArguments().First(),
                        Destination = type
                    }).ToList();

            var mapsTo = (
                    from type in types
                    from instance in type.GetInterfaces()
                    let i = instance
                    where
                        instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select new Map
                    {
                        Source = type,
                        Destination = i.GetGenericArguments().First()
                    }).ToList();

            mapsFrom.AddRange(mapsTo);
            return mapsFrom;
        }

        public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}

