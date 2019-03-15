﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.Xaml.Internals;

namespace Xamarin.Forms.Mocks.Xaml
{
    /// <summary>
    /// Originally from:
    ///     https://github.com/xamarin/Xamarin.Forms/blob/3.4.0/Xamarin.Forms.Core/Xaml/TypeConversionExtensions.cs
    ///     https://github.com/xamarin/Xamarin.Forms/blob/e46fef9190bbcf76e1fd7cd6f60d10d5a85d2f74/Xamarin.Forms.Core/Xaml/TypeConversionExtensions.cs
    /// Had to bring this code in because one of these internal methods was dropped
    /// </summary>
    public static class TypeConversionExtensions
    {
        internal static object ConvertTo (this object value, Type toType, Func<MemberInfo> minfoRetriever,
            IServiceProvider serviceProvider)
        {
            Func<object> getConverter = () => {
                MemberInfo memberInfo;

                var converterTypeName = toType.GetTypeInfo ().CustomAttributes.GetTypeConverterTypeName ();
                if (minfoRetriever != null && (memberInfo = minfoRetriever ()) != null)
                    converterTypeName = memberInfo.CustomAttributes.GetTypeConverterTypeName () ?? converterTypeName;
                if (converterTypeName == null)
                    return null;

                var convertertype = Type.GetType (converterTypeName);
                return Activator.CreateInstance (convertertype);
            };

            return ConvertTo (value, toType, getConverter, serviceProvider);
        }

        static string GetTypeConverterTypeName (this IEnumerable<CustomAttributeData> attributes)
        {
            var converterAttribute =
                attributes.FirstOrDefault (cad => TypeConverterAttribute.TypeConvertersType.Contains (cad.AttributeType.FullName));
            if (converterAttribute == null)
                return null;
            if (converterAttribute.ConstructorArguments [0].ArgumentType == typeof (string))
                return (string)converterAttribute.ConstructorArguments [0].Value;
            if (converterAttribute.ConstructorArguments [0].ArgumentType == typeof (Type))
                return ((Type)converterAttribute.ConstructorArguments [0].Value).AssemblyQualifiedName;
            return null;
        }

        internal static object ConvertTo (this object value, Type toType, Func<object> getConverter,
            IServiceProvider serviceProvider)
        {
            if (value == null)
                return null;

            var str = value as string;
            if (str != null) {
                //If there's a [TypeConverter], use it
                object converter = getConverter?.Invoke ();
                var xfTypeConverter = converter as TypeConverter;
                var xfExtendedTypeConverter = xfTypeConverter as IExtendedTypeConverter;
                if (xfExtendedTypeConverter != null)
                    return value = xfExtendedTypeConverter.ConvertFromInvariantString (str, serviceProvider);
                if (xfTypeConverter != null)
                    return value = xfTypeConverter.ConvertFromInvariantString (str);
                var converterType = converter?.GetType ();
                if (converterType != null) {
                    var convertFromStringInvariant = converterType.GetRuntimeMethod ("ConvertFromInvariantString",
                        new [] { typeof (string) });
                    if (convertFromStringInvariant != null)
                        return value = convertFromStringInvariant.Invoke (converter, new object [] { str });
                }

                //NOTE: only change here
                //var ignoreCase = (serviceProvider?.GetService (typeof (IConverterOptions)) as IConverterOptions)?.IgnoreCase ?? false;

                //If the type is nullable, as the value is not null, it's safe to assume we want the built-in conversion
                if (toType.GetTypeInfo ().IsGenericType && toType.GetGenericTypeDefinition () == typeof (Nullable<>))
                    toType = Nullable.GetUnderlyingType (toType);

                //Obvious Built-in conversions
                if (toType.GetTypeInfo ().IsEnum)
                    return Enum.Parse (toType, str, true);
                if (toType == typeof (SByte))
                    return SByte.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Int16))
                    return Int16.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Int32))
                    return Int32.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Int64))
                    return Int64.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Byte))
                    return Byte.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (UInt16))
                    return UInt16.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (UInt32))
                    return UInt32.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (UInt64))
                    return UInt64.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Single))
                    return Single.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Double))
                    return Double.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Boolean))
                    return Boolean.Parse (str);
                if (toType == typeof (TimeSpan))
                    return TimeSpan.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (DateTime))
                    return DateTime.Parse (str, CultureInfo.InvariantCulture);
                if (toType == typeof (Char)) {
                    char c = '\0';
                    Char.TryParse (str, out c);
                    return c;
                }
                if (toType == typeof (String) && str.StartsWith ("{}", StringComparison.Ordinal))
                    return str.Substring (2);
                if (toType == typeof (String))
                    return value;
                if (toType == typeof (Decimal))
                    return Decimal.Parse (str, CultureInfo.InvariantCulture);
            }

            //if the value is not assignable and there's an implicit conversion, convert
            if (value != null && !toType.IsAssignableFrom (value.GetType ())) {
                var opImplicit = value.GetType ().GetImplicitConversionOperator (fromType: value.GetType (), toType: toType)
                                ?? toType.GetImplicitConversionOperator (fromType: value.GetType (), toType: toType);

                if (opImplicit != null) {
                    value = opImplicit.Invoke (null, new [] { value });
                    return value;
                }
            }

            var nativeValueConverterService = DependencyService.Get<INativeValueConverterService> ();

            object nativeValue = null;
            if (nativeValueConverterService != null && nativeValueConverterService.ConvertTo (value, toType, out nativeValue))
                return nativeValue;

            return value;
        }

        internal static MethodInfo GetImplicitConversionOperator (this Type onType, Type fromType, Type toType)
        {
#if NETSTANDARD1_0
            var mi = onType.GetRuntimeMethod("op_Implicit", new[] { fromType });
            if (mi == null) return null;
            if (!mi.IsSpecialName) return null;
            if (!mi.IsPublic) return null;
            if (!mi.IsStatic) return null;
            if (!toType.IsAssignableFrom(mi.ReturnType)) return null;

            return mi;
#else
            var bindingAttr = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
            IEnumerable<MethodInfo> mis = null;
            try {
                mis = new [] { onType.GetMethod ("op_Implicit", bindingAttr, null, new [] { fromType }, null) };
            } catch (AmbiguousMatchException) {
                mis = new List<MethodInfo> ();
                foreach (var mi in onType.GetMethods (bindingAttr)) {
                    if (mi.Name != "op_Implicit") break;
                    var p = mi.GetParameters ()?.FirstOrDefault ();
                    if (p == null) continue;
                    if (!p.ParameterType.IsAssignableFrom (fromType)) continue;
                    ((List<MethodInfo>)mis).Add (mi);
                }
            }

            foreach (var mi in mis) {
                if (mi == null) continue;
                if (!mi.IsSpecialName) continue;
                if (!mi.IsPublic) continue;
                if (!mi.IsStatic) continue;
                if (!toType.IsAssignableFrom (mi.ReturnType)) continue;

                return mi;
            }
            return null;
#endif

        }
    }
}
