using System;
using System.Collections;
using System.Configuration;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public sealed class ErrorResponseInfo : IResponseInfo, IEquatable<ErrorResponseInfo>
    {
        public string Version { get; }

        public int Id { get; }
        public int AdvisoryDelay { get; }

        public int Code { get; }
        public string Message { get; }

        /// <summary>
        /// Create an instance of an <see cref="ErrorResponseInfo"/> with the properties populated
        /// </summary>
        /// <param name="version">Version of JSON-RPC returned</param>
        /// <param name="id">Request/Response unique identifier</param>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        public ErrorResponseInfo(string version, int id, int code, string message)
        {
            Version = version;

            Id = id;
            Code = code;
            Message = message;

            AdvisoryDelay = 0;
        }

        /// <summary>
        ///  Return an instance of an empty <see cref="ErrorResponseInfo"/>
        /// </summary>
        public static ErrorResponseInfo Empty(string version = null)
        {
            return new ErrorResponseInfo(version, 0, 0, string.Empty);
        }

        /// <summary>
        /// Determines if instance of <see cref="ErrorResponseInfo"/> is empty
        /// </summary>
        /// <returns>return <c>true</c> if the Code is empty and the Message is null, empty or contains only whitespace</returns>
        public bool IsEmpty()
        {
            return Code == 0 && string.IsNullOrWhiteSpace(Message);
        }

        /// <summary>
        /// Determines if this object and the passed in object are equal
        /// </summary>
        /// <param name="errorReponseInfo"><see cref="ErrorResponseInfo"/> in which to compare this instance</param>
        /// <returns>returns true if the two instances are equal</returns>
        public override bool Equals(object errorReponseInfo)
        {
            if (ReferenceEquals(errorReponseInfo, null))
                return false;

            if (ReferenceEquals(this, errorReponseInfo))
                return true;

            if (GetType() != errorReponseInfo.GetType())
                return false;

            return Equals(errorReponseInfo as ErrorResponseInfo);
        }

        #region IEquatable<ErrorResponseInfo> Members

        public bool Equals(ErrorResponseInfo errorReponseInfo)
        {
            return Code == errorReponseInfo.Code &&
                   Message == errorReponseInfo.Message &&
                   Version == errorReponseInfo.Version &&
                   Id == errorReponseInfo.Id;
        }

        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Version?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ Id;
                hashCode = (hashCode * 397) ^ Code;
                hashCode = (hashCode * 397) ^ (Message?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}