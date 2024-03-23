using System.Reflection;
using System.Runtime.CompilerServices;

namespace NCA.Common.Domain.Enums
{
    internal static class EnumObjectHelper
    {
        public static Exception ArgumentNullException(string paramName) => new ArgumentException(paramName);

        public static Exception ArgumentNullOrEmptyException(string paramName) => new ArgumentException("Argument cannot be null or empty.", paramName);

        public static Exception NameNotFoundException<TEnum, TValue>(string name)
            where TValue : IEquatable<TValue>, IComparable<TValue> => new ArgumentException($"No {typeof(TEnum).Name} with Name \"{name}\" found.");

        public static Exception ValueNotFoundException<TEnum, TValue>(TValue value)
            where TValue : IEquatable<TValue>, IComparable<TValue> => new ArgumentException($"No {typeof(TEnum).Name} with Value {value} found.");
    }

    internal static class EnumObjectExtensions
    {
        public static List<TFieldType> GetFieldsOfType<TFieldType>(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => type.IsAssignableFrom(p.FieldType))
                .Select(pi => (TFieldType)pi.GetValue(null)!)
                .ToList();
        }
    }

    public abstract class EnumObject<TEnum, TValue>
        where TEnum : EnumObject<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        static readonly Lazy<TEnum[]> _enumOptions = new Lazy<TEnum[]>(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

        static readonly Lazy<Dictionary<string, TEnum>> _fromName = new Lazy<Dictionary<string, TEnum>>(() => _enumOptions.Value.ToDictionary(item => item.Name));

        static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase = new Lazy<Dictionary<string, TEnum>>(
            () => _enumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase)
        );

        static readonly Lazy<Dictionary<TValue, TEnum>> _fromValue = new Lazy<Dictionary<TValue, TEnum>>(() =>
        {
            // multiple enums with same value are allowed but store only one per value
            var dictionary = new Dictionary<TValue, TEnum>();
            foreach (var item in _enumOptions.Value)
            {
                if (item._value != null && !dictionary.ContainsKey(item._value))
                    dictionary.Add(item._value, item);
            }
            return dictionary;
        });

        private static TEnum[] GetAllOptions()
        {
            Type baseType = typeof(TEnum);
            return Assembly.GetAssembly(baseType)!.GetTypes().Where(t => baseType.IsAssignableFrom(t)).SelectMany(t => t.GetFieldsOfType<TEnum>()).OrderBy(t => t.Name).ToArray();
        }

        /// <summary>
        /// Gets a collection containing all the instances of <see cref="EnumObject{TEnum, TValue}"/>.
        /// </summary>
        /// <value>A <see cref="IReadOnlyCollection{TEnum}"/> containing all the instances of <see cref="EnumObject{TEnum, TValue}"/>.</value>
        /// <remarks>Retrieves all the instances of <see cref="EnumObject{TEnum, TValue}"/> referenced by public static read-only fields in the current class or its bases.</remarks>
        public static IReadOnlyCollection<TEnum> List => _fromName.Value.Values.ToList().AsReadOnly();

        private readonly string _name;
        private readonly TValue _value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected EnumObject(string name, TValue value)
        {
            if (String.IsNullOrEmpty(name))
                throw EnumObjectHelper.ArgumentNullOrEmptyException(nameof(name));

            _name = name;
            _value = value;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>A <see cref="String"/> that is the name of the <see cref="EnumObject{TEnum, TValue}"/>.</value>
        public string Name => _name;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>A <typeparamref name="TValue"/> that is the value of the <see cref="EnumObject{TEnum, TValue}"/>.</value>
        public TValue Value => _value;

        /// <summary>
        /// Gets the item associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
        /// <returns>
        /// The item associated with the specified name.
        /// If the specified name is not found, throws a <see cref="KeyNotFoundException"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception>
        /// <exception cref="EnumObjectNotFoundException"><paramref name="name"/> does not exist.</exception>
        /// <seealso cref="EnumObject{TEnum, TValue}.TryFromName(string, out TEnum)"/>
        /// <seealso cref="EnumObject{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
        public static TEnum FromName(string name, bool ignoreCase = false)
        {
            if (String.IsNullOrEmpty(name))
                throw EnumObjectHelper.ArgumentNullOrEmptyException(nameof(name));

            if (ignoreCase)
                return FromName(_fromNameIgnoreCase.Value);
            else
                return FromName(_fromName.Value);

            TEnum FromName(Dictionary<string, TEnum> dictionary)
            {
                if (!dictionary.TryGetValue(name, out var result))
                {
                    throw EnumObjectHelper.NameNotFoundException<TEnum, TValue>(name);
                }
                return result;
            }
        }

        /// <summary>
        /// Gets the item associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        /// <param name="result">
        /// When this method returns, contains the item associated with the specified name, if the key is found;
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="EnumObject{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception>
        /// <seealso cref="EnumObject{TEnum, TValue}.FromName(string, bool)"/>
        /// <seealso cref="EnumObject{TEnum, TValue}.TryFromName(string, bool, out TEnum)"/>
        public static bool TryFromName(string name, out TEnum? result) => TryFromName(name, false, out result);

        /// <summary>
        /// Gets the item associated with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
        /// <param name="result">
        /// When this method returns, contains the item associated with the specified name, if the name is found;
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="EnumObject{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c>.</exception>
        /// <seealso cref="EnumObject{TEnum, TValue}.FromName(string, bool)"/>
        /// <seealso cref="EnumObject{TEnum, TValue}.TryFromName(string, out TEnum)"/>
        public static bool TryFromName(string name, bool ignoreCase, out TEnum? result)
        {
            if (String.IsNullOrEmpty(name))
            {
                result = default;
                return false;
            }

            if (ignoreCase)
                return _fromNameIgnoreCase.Value.TryGetValue(name, out result);
            else
                return _fromName.Value.TryGetValue(name, out result);
        }

        /// <summary>
        /// Gets an item associated with the specified value.
        /// </summary>
        /// <param name="value">The value of the item to get.</param>
        /// <returns>
        /// The first item found that is associated with the specified value.
        /// If the specified value is not found, throws a <see cref="KeyNotFoundException"/>.
        /// </returns>
        /// <exception cref="EnumObjectNotFoundException"><paramref name="value"/> does not exist.</exception>
        /// <seealso cref="EnumObject{TEnum, TValue}.FromValue(TValue, TEnum)"/>
        /// <seealso cref="EnumObject{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
        public static TEnum? FromValue(TValue value)
        {
            if (value == null)
                throw EnumObjectHelper.ArgumentNullException(nameof(value));

            TEnum? result;

            if (!_fromValue.Value.TryGetValue(value, out result))
            {
                throw EnumObjectHelper.ValueNotFoundException<TEnum, TValue>(value);
            }

            return result;
        }

        /// <summary>
        /// Gets an item associated with the specified value.
        /// </summary>
        /// <param name="value">The value of the item to get.</param>
        /// <param name="defaultValue">The value to return when item not found.</param>
        /// <returns>
        /// The first item found that is associated with the specified value.
        /// If the specified value is not found, returns <paramref name="defaultValue"/>.
        /// </returns>
        /// <seealso cref="EnumObject{TEnum, TValue}.FromValue(TValue)"/>
        /// <seealso cref="EnumObject{TEnum, TValue}.TryFromValue(TValue, out TEnum)"/>
        public static TEnum? FromValue(TValue value, TEnum defaultValue)
        {
            if (value == null)
                throw EnumObjectHelper.ArgumentNullException(nameof(value));

            if (!_fromValue.Value.TryGetValue(value, out var result))
            {
                return defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Gets an item associated with the specified value.
        /// </summary>
        /// <param name="value">The value of the item to get.</param>
        /// <param name="result">
        /// When this method returns, contains the item associated with the specified value, if the value is found;
        /// otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="EnumObject{TEnum, TValue}"/> contains an item with the specified name; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="EnumObject{TEnum, TValue}.FromValue(TValue)"/>
        /// <seealso cref="EnumObject{TEnum, TValue}.FromValue(TValue, TEnum)"/>
        public static bool TryFromValue(TValue value, out TEnum? result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }

            return _fromValue.Value.TryGetValue(value, out result);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => _name;
    }
}
