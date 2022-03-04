namespace MinimalApiPlayground.Framework.Models
{
    public class InternalError
    {
        public string? Descricao { get; private set; }
        public string CodigoInterno { get; private set; }
        public string Trace { get; private set; }

        public InternalError(string codigoInterno, string trace)
        {
            CodigoInterno = codigoInterno;
            Trace = trace;
        }

        public InternalError(string descricao, string codigoInterno, string trace)
        {
            Descricao = descricao;
            CodigoInterno = codigoInterno;
            Trace = trace;
        }
    }
}
