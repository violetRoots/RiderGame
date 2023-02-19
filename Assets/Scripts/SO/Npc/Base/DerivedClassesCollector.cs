using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RiderGame.SO
{
    public abstract class DerivedClassesCollector<T>
    {
        public static Dictionary<string, Type> All
        {
            get
            {
                if (modifiers == null)
                    modifiers = GetModifierTypes();
                return modifiers;
            }
        }

        private static Dictionary<string, Type> modifiers;
        private static Dictionary<string, Type> GetModifierTypes()
        {
            var derivedClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => typeof(T).IsAssignableFrom(type)
                ).ToArray();

            var res = new Dictionary<string, Type>();
            foreach (var derivedClass in derivedClasses)
            {
                if (derivedClass == typeof(T)) continue;

                res.Add(derivedClass.Name, derivedClass);
            }

            return res;
        }
    }
}
