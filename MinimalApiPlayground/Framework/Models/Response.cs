using MinimalApiPlayground.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Net;

namespace MinimalApiPlayground.Framework.Models
{
    public class Response<T> : Response
    {
        public T? Payload { get; private set; }
        
        public Response(HttpStatusCode http, string mensagem, T? obj) : base(http, mensagem)
        {
            Payload = obj;
        }

        public Response(HttpStatusCode http, string mensagem, InternalError erro, T? obj) : base(http, mensagem, erro)
        {
            Payload = obj;
        }


    }

    public class Response
    {
        #region Properties
        public HttpStatusCode Http { get; protected set; }
        public string Mensagem { get; protected set; }
        public bool RequisicaoOK { get { return Erro is null; } }
        public InternalError? Erro { get; protected set; }

        #endregion

        #region Constructors
        public Response(HttpStatusCode http, string mensagem)
        {
            Http = http;
            Mensagem = mensagem;
        }
        public Response(HttpStatusCode http, string mensagem, InternalError erro)
        {
            Http = http;
            Mensagem = mensagem;
            Erro = erro;
        }
        #endregion

        #region Static Methods
        public static Response<List<Y>> EmptyList<Y>()
        {
            return new Response<List<Y>>(HttpStatusCode.NoContent, "O registro não foi encontrado.", new List<Y>());
        }

        public static Response<Y> Empty<Y>()
        {
            var obj = (Y) Activator.CreateInstance(typeof(Y));

            return new Response<Y>(HttpStatusCode.NoContent, "O registro não foi encontrado.", obj);
        }

        public static Response Ok(string mensagem)
        {
            return new Response(HttpStatusCode.OK, mensagem);
        }

        public static Response Ok()
        {
            return new Response(HttpStatusCode.OK, "Requisição concluída com sucesso.");
        }

        public static Response<Y> Ok<Y>(string mensagem, Y obj)
        {
            return new Response<Y>(HttpStatusCode.OK, mensagem, obj);
        }

        public static Response<Y> Ok<Y>(Y obj)
        {
            return new Response<Y>(HttpStatusCode.OK, "Requisição concluída com sucesso.", obj);
        }
        #endregion
    }
}