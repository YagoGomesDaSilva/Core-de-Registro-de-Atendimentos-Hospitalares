using System;
namespace Domain.Exceptions 
{
    public class DomainException : System.Exception // Especifica o tipo Exception totalmente qualificado
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
