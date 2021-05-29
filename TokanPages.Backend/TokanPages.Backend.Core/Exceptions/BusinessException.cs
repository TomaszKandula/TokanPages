﻿using System;
using System.Runtime.Serialization;

namespace TokanPages.Backend.Core.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }

        protected BusinessException(SerializationInfo ASerializationInfo, StreamingContext AStreamingContext) { }

        public BusinessException(string AErrorCode, string AErrorMessage = "") : base(AErrorMessage)
            => ErrorCode = AErrorCode;
    }
}
