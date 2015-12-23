using System;
using System.Runtime.Serialization;

namespace Obacher.RandomOrgSharp
{
    /// <summary>
    /// Exceptions that occur during the API request to api.random.org
    /// </summary>
    [Serializable]
    public class RandomOrgException : Exception
    {
        public int Code { get; private set; }

        public RandomOrgException(int code, string message) : base(message)
        {
            Code = code;
        }

        public RandomOrgException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        // Make sure you include this so your Exception is properly Serializable
        protected RandomOrgException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }
    }

    /// <summary>
    /// Exceptions that occur within <see cref="Obacher.RandomOrgSharp"/>.
    /// </summary>
    [Serializable]
    public class RandomOrgRunTimeException : Exception
    {
        public RandomOrgRunTimeException(string message) : base(message) { }

        public RandomOrgRunTimeException(string message, Exception innerException) : base(message, innerException) { }

        protected RandomOrgRunTimeException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }
    }
}


