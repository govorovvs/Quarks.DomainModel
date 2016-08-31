﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Quarks.DomainModel.Building
{
    /// <summary>
    /// Customizes the creation algorithm for a single object.
    /// </summary>
    /// <typeparam name="T">The type of object for which the algorithm should be customized.</typeparam>
    public class Builder<T> where T : class
    {
        private readonly T _instance;

        /// <summary>
        /// Initializes a new instance of <see cref="Builder{T}"/> class.
        /// </summary>
        public Builder()
        {
            _instance = CreateInstance();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Builder{T}"/> class with exsiting instance.
        /// </summary>
        /// <param name="instance">The instance</param>
        public Builder(T instance)
        {
            _instance = instance;
        }

        /// <summary>
        /// Registers that a writable property or field should be assigned a specific value.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property of field.</typeparam>
        /// <param name="propertyPicker"> 
        /// An expression that identifies the property or field that will have 
        /// <paramref name="value" /> assigned.</param>
        /// <param name="value">The value to assign to the property or field identified by
        /// <paramref name="propertyPicker" />.</param>
        /// <returns></returns>
        public Builder<T> With<TProperty>(Expression<Func<T, TProperty>> propertyPicker, TProperty value)
        {
            if (_instance == null)
                return this;

            MemberExpression memberExpression = propertyPicker.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Only property and field can be populated via With");

            PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(_instance, value);
                return this;
            }
            FieldInfo fieldInfo = memberExpression.Member as FieldInfo;
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(_instance, value);
                return this;
            }

            return this;
        }

        /// <summary>
        /// Creates an anonymous variable of <typeparamref name="T"/> type.
        /// </summary>
        /// <returns>Created instance.</returns>
        public T Create()
        {
            return _instance;
        }

        private T CreateInstance()
        {
            ConstructorInfo parameterlessConstructor =
                typeof(T).GetTypeInfo().DeclaredConstructors
                    .SingleOrDefault(constructor => constructor.GetParameters().Any() == false);
            if (parameterlessConstructor == null)
            {
                throw new MissingMethodException($"Type {typeof(T).Name} doesn't contain parameterless constructor");
            }

            return (T) parameterlessConstructor.Invoke(new object[0]);
        }
    }
}