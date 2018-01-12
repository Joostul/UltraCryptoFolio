using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Extensions
{
    public class BaseSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Transaction).IsAssignableFrom(objectType) && !objectType.GetTypeInfo().IsAbstract)
            {
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            }

            return base.ResolveContractConverter(objectType);
        }
    }
}
