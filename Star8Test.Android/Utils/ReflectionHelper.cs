using System;
using System.Reflection;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{

    public static class ReflectionHelper
    {
        public static void RaiseInstanceEvent<TEventArgs>(this object source, string eventName, TEventArgs eventArgs)
            where TEventArgs : EventArgs
        {
            MulticastDelegate eventDelegate = (MulticastDelegate)source.GetType()
                .GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(source);
            if (eventDelegate != null)
            {
                foreach (Delegate handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { source, eventArgs });
                }
            }
        }

        public static void RaiseStaticEvent<TEventArgs>(this Type ownerType, string eventName, TEventArgs eventArgs)
            where TEventArgs : EventArgs
        {
            MulticastDelegate eventDelegate = (MulticastDelegate)ownerType
                .GetField(eventName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).GetValue(null);
            if (eventDelegate != null)
            {
                foreach (Delegate handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { ownerType, eventArgs });
                }
            }
        }

        private static PropertyInfo GetStaticPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propertyInfo;
            do
            {
                propertyInfo = type.GetProperty(propertyName,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (propertyInfo == null && type != null);
            return propertyInfo;
        }

        public static void SetStaticPropertyValue(this Type type, string propertyName, object val)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            PropertyInfo propertyInfo = GetStaticPropertyInfo(type, propertyName);
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException(nameof(propertyInfo),
                    string.Format("Couldn't find property {0} in type {1}", propertyName, type.FullName));
            propertyInfo.SetValue(null, val);
        }

        public static object GetStaticPropertyValue(this Type type, string propertyName)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            PropertyInfo propertyInfo = GetStaticPropertyInfo(type, propertyName);
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException(nameof(propertyName),
                    string.Format("Couldn't find property {0} in type {1}", propertyName, type.FullName));
            return propertyInfo.GetValue(null);
        }

        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propertyInfo;
            do
            {
                propertyInfo = type.GetProperty(propertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (propertyInfo == null && type != null);
            return propertyInfo;
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            Type objType = obj.GetType();
            PropertyInfo propertyInfo = GetPropertyInfo(objType, propertyName);
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException(nameof(propertyName),
                    string.Format("Couldn't find property {0} in type {1}", propertyName, objType.FullName));
            return propertyInfo.GetValue(obj);
        }

        public static void SetPropertyValue(this object obj, string propertyName, object val)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            Type objType = obj.GetType();
            PropertyInfo propertyInfo = GetPropertyInfo(objType, propertyName);
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException(nameof(propertyInfo),
                    string.Format("Couldn't find property {0} in type {1}", propertyName, objType.FullName));
            propertyInfo.SetValue(obj, val);
        }

        private static FieldInfo GetStaticFieldInfo(Type type, string fieldName)
        {
            FieldInfo fieldInfo;
            do
            {
                fieldInfo = type.GetField(fieldName,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (fieldInfo == null && type != null);
            return fieldInfo;
        }

        public static void SetStaticFieldValue(this Type type, string fieldName, object val)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            FieldInfo fieldInfo = GetStaticFieldInfo(type, fieldName);
            if (fieldInfo == null)
                throw new ArgumentOutOfRangeException(nameof(fieldInfo),
                    string.Format("Couldn't find field {0} in type {1}", fieldName, type.FullName));
            fieldInfo.SetValue(null, val);
        }

        public static object GetStaticFieldValue(this Type type, string fieldName)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            FieldInfo fieldInfo = GetStaticFieldInfo(type, fieldName);
            if (fieldInfo == null)
                throw new ArgumentOutOfRangeException(nameof(fieldName),
                    string.Format("Couldn't find field {0} in type {1}", fieldName, type.FullName));
            return fieldInfo.GetValue(null);
        }

        private static FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            FieldInfo fieldInfo;
            do
            {
                fieldInfo = type.GetField(fieldName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            while (fieldInfo == null && type != null);
            return fieldInfo;
        }

        public static object GetFieldValue(this object obj, string fieldName)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            Type objType = obj.GetType();
            FieldInfo fieldInfo = GetFieldInfo(objType, fieldName);
            if (fieldInfo == null)
                throw new ArgumentOutOfRangeException(nameof(fieldName),
                    string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
            return fieldInfo.GetValue(obj);
        }

        public static void SetFieldValue(this object obj, string fieldName, object val)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            Type objType = obj.GetType();
            FieldInfo fieldInfo = GetFieldInfo(objType, fieldName);
            if (fieldInfo == null)
                throw new ArgumentOutOfRangeException(nameof(fieldInfo),
                    string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
            fieldInfo.SetValue(obj, val);
        }
    }
}