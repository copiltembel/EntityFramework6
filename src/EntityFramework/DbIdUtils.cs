using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity
{
    public static class DbIdUtils
    {

        static ConcurrentDictionary<Type, Func<int, object>> dbEntityCreatorCache = new ConcurrentDictionary<Type, Func<int, object>>();
        static object CreateDbIdGenericInstance(Type dbEntityGenericParameterType, int parameter)
        {
            Func<int, object> instanceCreator = dbEntityCreatorCache.GetOrAdd(dbEntityGenericParameterType, (type) => { return GetInstanceCreator(type); });
            return instanceCreator(parameter);
        }

        private static Func<int, object> GetInstanceCreator(Type dbEntityGenericParameterType)
        {
            var genericType = typeof(DbId<>).MakeGenericType(dbEntityGenericParameterType);
            var constructorInfo = genericType.GetConstructors()[0];
            var parameterExpression = Expression.Parameter(typeof(int));
            var expression = Expression.New(constructorInfo, parameterExpression);
            var conversion = Expression.Convert(expression, typeof(object));
            var instanceCreator = Expression.Lambda<Func<int, object>>(conversion, parameterExpression).Compile();
            return instanceCreator;
        }

        public static bool TryGetDbId<TTarget>(Type targetType, object value, out TTarget result)
        {
            if (targetType.IsGenericType())
            {
                if (targetType.GenericTypeArguments.Count() == 1)
                {
                    if (targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var targetTypeNonNullable = targetType.GenericTypeArguments[0];
                        GetNonNullableDbId(targetTypeNonNullable, value, out object nonNullableResult);
                        var nullableDbIdType = typeof(Nullable<>).MakeGenericType(targetTypeNonNullable);
                        result = (TTarget)Activator.CreateInstance(nullableDbIdType, nonNullableResult);
                        return true;
                    }
                    if (targetType.GetGenericTypeDefinition() == typeof(DbId<>))
                    {
                        return GetNonNullableDbId(targetType, value, out result);
                    }
                }
            }
            result = default;
            return false;
        }

        static bool GetNonNullableDbId<TTarget>(Type targetType, object value, out TTarget result)
        {
            var dbEntityType = targetType.GenericTypeArguments[0];
            result = (TTarget)CreateDbIdGenericInstance(dbEntityType, (int)(object)value);
            return true;
        }
    }
}
