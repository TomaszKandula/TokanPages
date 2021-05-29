using System;

namespace TokanPages.Backend.Core.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }

        public BusinessException(string AErrorCode, string AErrorMessage = "") : base(AErrorMessage)
            => ErrorCode = AErrorCode;
    }
}
