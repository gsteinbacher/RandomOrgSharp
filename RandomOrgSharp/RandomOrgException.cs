using System;
using System.Runtime.Serialization;

namespace Obacher.RandomOrgSharp
{
    [Serializable]
    public class RandomOrgException : Exception
    {
        public int Code { get; private set; }
        public RandomOrgException(int code, string message)
            : base(message)
        { Code = code; }

        public RandomOrgException(int code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        // Make sure you include this so your Excpetion is properly Serializable
        protected RandomOrgException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }

    [Serializable]
    public class RandomOrgRunTimeException : Exception
    {
        public int Code { get; private set; }

        public RandomOrgRunTimeException(int code, string message)
            : base(message)
        { Code = code; }


        public RandomOrgRunTimeException(string message)
            : base(message)
        { Code = 9999; }

        public RandomOrgRunTimeException(string message, Exception innerException)
            : base(message, innerException)
        { Code = 9999; }

        public RandomOrgRunTimeException(int code, string message, Exception innerException)
            : base(message, innerException)
        { Code = code; }

        protected RandomOrgRunTimeException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }
}


