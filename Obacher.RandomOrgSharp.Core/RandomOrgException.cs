using System;
using System.Runtime.Serialization;

namespace Obacher.RandomOrgSharp.Core
{
    /// <summary>
    /// Exceptions that occur during the API request to api.random.org
    /// </summary>
    [Serializable]
    public class RandomOrgException : Exception
    {
        public int Code { get; }

        public RandomOrgException()
        {
            Code = 32000;
        }

        public RandomOrgException(int code, string message) : base(message)
        {
            Code = code;
        }

        public RandomOrgException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        // Make sure you include this so your Exception is properly Serializable
        protected RandomOrgException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            Code = info.GetInt32("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", Code, typeof(int));
            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// Exceptions that occur within <see cref="Obacher.RandomOrgSharp"/>.
    /// </summary>
    [Serializable]
    public class RandomOrgRuntimeException : Exception
    {
        public RandomOrgRuntimeException() { }
        public RandomOrgRuntimeException(string message) : base(message) { }
        public RandomOrgRuntimeException(string message, Exception innerException) : base(message, innerException) { }
        protected RandomOrgRuntimeException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }
    }
}


