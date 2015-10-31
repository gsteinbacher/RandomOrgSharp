using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Obacher.UnitTest.Tools
{
    public static class UnitTestHelper
    {
        public static T GetStaticPrivatePropertyByType<T>(Type type, string propertyName)
        {
            PrivateType pt = new PrivateType(type);
            return (T)pt.GetStaticFieldOrProperty(propertyName, BindingFlags.NonPublic);
        }

        public static void SetStaticPrivatePropertyByType(Type type, string propertyName, object value)
        {
            new PrivateType(type).SetStaticFieldOrProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Static, value);
        }

        public static T GetPrivateProperty<T>(object instance, string propertyName)
        {
            PrivateObject po = new PrivateObject(instance);
            return (T)po.GetFieldOrProperty(propertyName);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static T GetBasePrivateProperty<T>(object instance, string propertyName)
        {
            FieldInfo info = instance.GetType().BaseType.GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)info.GetValue(instance);
        }

        public static void SetPrivateProperty(object instance, string propertyName, object value)
        {
            new PrivateObject(instance).SetFieldOrProperty(propertyName, value);
        }

        public static T GetStaticPrivateProperty<T>(object instance, string propertyName)
        {
            PrivateObject po = new PrivateObject(instance);
            return (T)po.GetField(propertyName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        }

        public static void SetStaticPrivateProperty(object instance, string propertyName, object value)
        {
            new PrivateObject(instance).SetFieldOrProperty(propertyName, BindingFlags.Static | BindingFlags.NonPublic, value);
        }

        // Does the same thing as GetStaticPrivate but wanted to create a separate method for easier understanding
        public static T GetPrivateConstant<T>(object instance, string propertyName)
        {
            return GetStaticPrivateProperty<T>(instance, propertyName);
        }

        /// <summary>
        /// Execute a static method through reflection.  Used primarily to execute private methods
        /// </summary>
        /// <param name="type">Type of object which contains method</param>
        /// <param name="methodName">Method to execute</param>
        /// <param name="parameters">Parameters to pass into method</param>
        /// <returns>Return value of method</returns>
        public static T ExecuteStaticMethod<T>(Type type, string methodName, params object[] parameters)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public;
            PrivateType pt = new PrivateType(type);
            return (T)pt.InvokeStatic(methodName, bindingFlags, parameters);
        }

        /// <summary>
        /// Execute a method through reflection.  Used primarily to execute static private methods.
        /// </summary>
        /// <param name="type">Type of object which contains method</param>
        /// <param name="methodName">Method to execute</param>
        /// <param name="bindingFlags">Binding flags</param>
        /// <param name="parameters">Parameters to pass into method</param>
        /// <returns>Return value of method</returns>
        public static T ExecuteMethodByType<T>(Type type, string methodName, BindingFlags bindingFlags, params object[] parameters)
        {
            PrivateObject instance = new PrivateObject(type);
            return (T)instance.Invoke(methodName, bindingFlags, parameters);
        }

        /// <summary>
        /// Execute a method through reflection.  Used primarily to execute private methods
        /// </summary>
        /// <param name="instance">Type of object which contains method, call "GetType()" on your instance of class being tested</param>
        /// <param name="methodName">Method to execute</param>
        /// <param name="parameters">Parameters to pass into method</param>
        /// <returns>Return value of method</returns>
        public static T ExecutePrivateMethod<T>(object instance, string methodName, params object[] parameters)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            return ExecuteMethod<T>(instance, methodName, bindingFlags, parameters);
        }

        /// <summary>
        /// Execute a static method through reflection.  Used primarily to execute private methods
        /// </summary>
        /// <param name="instance">Instance of object which contains method</param>
        /// <param name="methodName">Method to execute</param>
        /// <param name="bindingFlags">Binding flags</param>
        /// <param name="parameters">Parameters to pass into method</param>
        /// <returns>Return value of method</returns>
        public static T ExecuteMethod<T>(object instance, string methodName, BindingFlags bindingFlags, params object[] parameters)
        {
            PrivateObject privateObject = new PrivateObject(instance);
            return (T)privateObject.Invoke(methodName, bindingFlags, parameters);
        }

        /// <summary>
        /// Execute a static method through reflection.  Used primarily to execute private methods
        /// </summary>
        /// <param name="type">Type of object which contains method</param>
        /// <param name="methodName">Method to execute</param>
        /// <param name="parameters">Parameters to pass into method</param>
        /// <returns>Return value of method</returns>
        public static void ExecuteStaticMethod(Type type, string methodName, params object[] parameters)
        {
            ExecuteMethod(type, methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public, parameters);
        }

        /// <summary>
        /// Execute a method through reflection.  Used primarily to execute private methods
        /// </summary>
        /// <param name="type">Type of object which contains method</param>
        /// <param name="methodName">Method to execute</param>
        /// <param name="bindingFlags">Binding flags</param>
        /// <param name="parameters">Parameters to pass into method</param>
        /// <returns>Return value of method</returns>
        public static void ExecuteMethod(Type type, string methodName, BindingFlags bindingFlags, params object[] parameters)
        {
            new PrivateObject(type).Invoke(methodName, bindingFlags, parameters);
        }

    }
}
