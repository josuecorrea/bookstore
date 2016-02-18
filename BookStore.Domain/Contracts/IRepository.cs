using System;
using System.Collections.Generic;

namespace BookStore.Domain.Contracts
{
    public interface IRepository<T> : IDisposable
    {
        //evitar com o usuario não traga um volume muito grande de informações
        //sem saber o que ta fazendo.        
        List<T> Get(int skip = 0, int take = 25);

        T Get(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}