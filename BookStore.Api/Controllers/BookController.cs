﻿using BookStore.Domain;
using BookStore.Domain.Contracts;
using BookStore.Utils.Attributes;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace BookStore.Api.Controllers
{
    [RoutePrefix("api/public/v1")]//perde a configuração original de rotas.
    public class BookController : ApiController
    {
        //private IBookRepository _repository = new BookRepository();
        private IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        //[EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [DeflateCompression]
        [BasicAuthenticationAttibute]
        [Route("livros")]
        public Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _repository.GetWithAuthors();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar livros");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        //[DeflateCompression]
        [Route("livros/{id}")]
        public Task<HttpResponseMessage> GetById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _repository.GetWithAuthors(id);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar livros");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        //[DeflateCompression]
        [Route("livros/{id}/autores")]
        public Task<HttpResponseMessage> GetAuthors(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                //var result = _repository.GetWithAuthors(id).Authors.ToList();
                var result = _repository.GetWithAuthors(id).ToList();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar autores");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        #region Create

        [HttpPost]
        [Route("livros")]
        public Task<HttpResponseMessage> Post(Book book)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _repository.Create(book);
                response = Request.CreateResponse(HttpStatusCode.Created, book);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar livros");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        #endregion Create

        #region Update

        [HttpPut]
        [Route("livros")]
        public Task<HttpResponseMessage> Put(Book book)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _repository.Update(book);
                response = Request.CreateResponse(HttpStatusCode.OK, book);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar livros");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        #endregion Update

        #region Delete

        [HttpDelete]
        [Route("livros/{id}")]
        public Task<HttpResponseMessage> Delete(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _repository.Delete(id);
                response = Request.CreateResponse(HttpStatusCode.OK, "Livro removido com sucesso!");
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar livros");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        #endregion Delete

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
        }
    }
}