namespace TokanPages.Backend.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [Serializable]
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }

        protected BusinessException(SerializationInfo ASerializationInfo, 
            StreamingContext AStreamingContext) : base(ASerializationInfo, AStreamingContext) { }

        public BusinessException(string AErrorCode, string AErrorMessage = "") : base(AErrorMessage)
            => ErrorCode = AErrorCode;
    }
}