using MinimalApiPlayground.Framework.Models;
using System.Net;

namespace MinimalApiPlayground.Framework.Extensions
{
    public static class ObjectExtensions
    {
        public static Response<T> ToResponseOkWithMessage<T>(this T obj, string mensagem)
        {
            return new Response<T>(HttpStatusCode.OK, mensagem, obj);
        }

        public static Response<T> ToResponseOk<T>(this T obj)
        {
            return new Response<T>(HttpStatusCode.OK, "Requisição concluida com sucesso.", obj);
        }
    }
}
