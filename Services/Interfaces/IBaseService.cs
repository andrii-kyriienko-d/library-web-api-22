using LibraryWebApi.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace LibraryWebApi.Services.Interfaces;

public interface IBaseService<T>
{
    public int Create(T entity);
    public void Delete(int id);
    public T Get(int id);
    public IEnumerable<T> GetEntities();
    public void Update(int id, T entity);
    public IEnumerable<T> Find(SearchModel searchExpression);
    public void ImportDataFromExcel(IFormFile file);
    public void ImportDataFromJson(IFormFile file);
    public Stream ExportDataToJson();
    public Stream ExportDataToExcel();
}