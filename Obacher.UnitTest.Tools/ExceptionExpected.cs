using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Obacher.UnitTest.Tools
{
    public enum ComparisonType
    {
        Contains,
        EndsWith,
        Exact,
        StartsWith
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ExceptionExpectedAttribute : ExpectedExceptionBaseAttribute
    {
        /// <summary>Gets the expected exception type.</summary>
        /// <returns>A <see cref="T:System.Type" /> object.</returns>
        public Type ExceptionType { get; }

        /// <summary>Gets the expected exception message.  Comparison is case-insensitive and all spaces are ignored.</summary>
        /// <returns>A <see cref="T:System.String" /> object.</returns>
        public string ExceptionMessage { get; }

        /// <summary>Determines the manner in which the message is evaluated.</summary>
        /// <returns>A <see cref="T:System.String" /> object.</returns>
        public ComparisonType ExceptionComparisonType { get; }

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute" /> class with and expected exception type and a message that describes the exception.</summary>
        /// <param name="exceptionType">An expected type of exception to be thrown by a method.</param>
        /// <param name="expectedMessage">Message expected to be returned in exception</param>
        public ExceptionExpectedAttribute(Type exceptionType, string expectedMessage)
            : this(exceptionType, expectedMessage, ComparisonType.Contains, string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute" /> class with and expected exception type and a message that describes the exception.</summary>
        /// <param name="exceptionType">An expected type of exception to be thrown by a method.</param>
        /// <param name="expectedMessage">Message expected to be returned in exception</param>
        /// <param name="comparisonType">The manner in which the message is evaluated</param>
        public ExceptionExpectedAttribute(Type exceptionType, string expectedMessage, ComparisonType comparisonType)
            : this(exceptionType, expectedMessage, comparisonType, string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute" /> class with and expected exception type and a message that describes the exception.</summary>
        /// <param name="exceptionType">An expected type of exception to be thrown by a method.</param>
        /// <param name="expectedMessage">Message expected to be returned in exception</param>
        /// <param name="comparisonType">The manner in which the message is evaluated</param>
        /// <param name="noExceptionMessage">The message to be displayed when no exception occurs in test</param>
        public ExceptionExpectedAttribute(Type exceptionType, string expectedMessage, ComparisonType comparisonType, string noExceptionMessage)
            : base(noExceptionMessage)
        {
            if (exceptionType == null)
                throw new ArgumentNullException(nameof(exceptionType));

            if (string.IsNullOrEmpty(expectedMessage))
                throw new ArgumentNullException(nameof(expectedMessage));

            ExceptionType = exceptionType;
            ExceptionMessage = expectedMessage;
            ExceptionComparisonType = comparisonType;
        }

        protected override void Verify(Exception exception)
        {
            Type type = exception.GetType();
            if (type != ExceptionType)
            {
                RethrowIfAssertException(exception);
                throw new Exception(@"Wrong exception thrown");
            }

            string exceptionMessage = exception.Message.Replace(" ", "").ToLower();
            string exceptionMessageCompare = ExceptionMessage.Replace(" ", "").ToLower();

            switch (ExceptionComparisonType)
            {
                case ComparisonType.Contains:
                    if (!exceptionMessage.Contains(exceptionMessageCompare))
                        throw new Exception($"Expected message <{exception.Message}> does not contain <{ExceptionMessage}>");
                    break;
                case ComparisonType.Exact:
                    if (String.Compare(exceptionMessage, exceptionMessageCompare, StringComparison.Ordinal) != 0)
                        throw new Exception($"Expected message not found: Expected <{ExceptionMessage}>, Actual<{exception.Message}>");
                    break;
                case ComparisonType.EndsWith:
                    if (!exceptionMessage.EndsWith(exceptionMessageCompare))
                        throw new Exception($"Expected message <{exception.Message}> does not end with <{ExceptionMessage}>");
                    break;
                case ComparisonType.StartsWith:
                    if (!exceptionMessage.StartsWith(exceptionMessageCompare))
                        throw new Exception($"Expected message <{exception.Message}> does not start with <{ExceptionMessage}>");
                    break;
            }
        }
    }
}
