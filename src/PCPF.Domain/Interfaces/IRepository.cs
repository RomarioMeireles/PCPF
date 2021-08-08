﻿using PCPF.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PCPF.Domain.Interfaces
{
    public interface IRepository<TEntity>: IDisposable where  TEntity:Entity
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(int id);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(int id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
        Task AddRange(IEnumerable<TEntity> entities);
        Task RemoveRange(IEnumerable<TEntity> entities);
    }
}
