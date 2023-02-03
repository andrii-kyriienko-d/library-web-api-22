using LibraryWebApi.Models.RequestModels;
using System.Collections.Generic;

namespace LibraryWebApi.Repositories.Interfaces;

internal interface IRepository<T>
{
    int Create(T entity);
    void Delete(int id);
    T Get(int id);
    IEnumerable<T> GetEntities();
    void Update(T entity);
    IEnumerable<T> Find(SearchModel findExpression);

}