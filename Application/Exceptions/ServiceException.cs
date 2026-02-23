using System;

namespace Application.Exceptions
{
    /// <summary>
    /// Exceção base para erros conhecidos na camada de serviço.
    /// Resulta em HTTP 400 (Bad Request) quando tratada pela apresentação.
    /// </summary>
    public class ServiceException : Exception
    {
        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}