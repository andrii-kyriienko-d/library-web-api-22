using LibraryWebApi.Models.RequestModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace LibraryWebApi.Services.Interfaces;

public interface IBaseService<T>
{
    int Create(T entity);
    void Delete(int id);
    T Get(int id);
    IEnumerable<T> GetEntities();
    void Update(int id, T entity);
    IEnumerable<T> Find(SearchModel searchExpression);
    void ImportDataFromExcel(IFormFile file);
    void ImportDataFromJson(IFormFile file);
    Stream ExportDataToJson();
    Stream ExportDataToExcel();
}