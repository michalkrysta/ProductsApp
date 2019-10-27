using System;

namespace ProductsApp.Core.Domain
{
    public abstract class AppException : Exception
    {
        protected AppException()
        {
        }

        protected AppException(string code)
        {
            Code = code;
        }

        protected AppException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected AppException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected AppException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected AppException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }

        public string Code { get; }
    }
}