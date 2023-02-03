using LibraryWebApi.Models.RequestModels;
using System.Collections.Generic;

namespace LibraryWebApi.Repositories.Interfaces;

public interface IRepository<T>
{
    public int Create(T entity);
    public void Delete(int id);
    public T Get(int id);
    public IEnumerable<T> GetEntities();
    public void Update(T entity);
    public IEnumerable<T> Find(SearchModel findExpression);

}