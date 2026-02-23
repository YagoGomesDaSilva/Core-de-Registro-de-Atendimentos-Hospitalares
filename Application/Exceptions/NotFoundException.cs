namespace Application.Exceptions
{
    /// <summary>
    /// Exceção para entidades não encontradas na camada de serviço.
    /// Resulta em HTTP 404 (Not Found) quando tratada pela apresentação.
    /// </summary>
    public class NotFoundException : ServiceException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entidade, int id)
            : base($"{entidade} com ID {id} não foi encontrado(a).") { }
    }
}