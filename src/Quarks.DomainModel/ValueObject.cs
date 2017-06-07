using System;
using System.Collections.Generic;
using System.Reflection;

namespace Quarks.DomainModel
{
    /// <summary>
    /// An object that contains attributes but has no conceptual identity. They should be treated as immutable.
    /// </summary>
    /// <see href="http://martinfowler.com/bliki/ValueObject.html"/>
    [ValueObject]
    public abstract class ValueObject<T> : IEquatable<T>, IValueObject
        where T : ValueObject<T>
    {
        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((T)obj);
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        public virtual bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;

            IEnumerable<FieldInfo> fields = GetFields();

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (Equals(value1, value2) == false)
                    return false;
            }

            return true;
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int hashCode = 17;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * 59 + value.GetHashCode();
            }

            return hashCode;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type type = GetType();

            List<FieldInfo> fields = new List<FieldInfo>();

            while (type != typeof(object))
            {
                fields.AddRange(type.GetTypeInfo().DeclaredFields);

                type = type.GetTypeInfo().BaseType;
            }

            return fields;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}