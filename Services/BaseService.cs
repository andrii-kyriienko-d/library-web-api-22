using ClosedXML.Excel;
using LibraryWebApi.Entities.Interfaces;
using LibraryWebApi.Models.RequestModels;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using LibraryWebApi.Extensions;
using ToDataTable;

namespace LibraryWebApi.Services;

internal abstract class BaseService<T> : IBaseService<T> where T : class, IEntityId, new()
{
    private readonly IRepository<T> _entityRepository;
    private readonly IMemoryCache _cache;

    protected BaseService(IRepository<T> entityRepository, IMemoryCache cache)
    {
        _entityRepository = entityRepository;
        _cache = cache;
    }

    public int Create(T entity)
    {
        return _entityRepository.Create(entity);
    }

    public void Delete(int id)
    {
        _entityRepository.Delete(id);
    }

    public T Get(int id)
    {
        T entity;
        if (!_cache.TryGetValue($"{id}_{typeof(T).Name}", out entity))
        {
            entity = _entityRepository.Get(id);
            if (entity != null)
            {
                _cache.Set($"{id}_{typeof(T).Name}", entity,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));
            }
        }
        return entity;
    }

    public IEnumerable<T> GetEntities()
    {
        return _entityRepository.GetEntities();
    }

    public void Update(int id, T entity)
    {
        entity.Id = id;
        _entityRepository.Update(entity);
    }
    public IEnumerable<T> Find(SearchModel searchExpression)
    {
        return _entityRepository
            .Find(searchExpression)
            .Distinct();
    }

    public Stream ExportDataToExcel()
    {
        var excelStream = new MemoryStream();

        using (var workbook = new XLWorkbook())
        {
            var dataToSave = GetEntities().ToDataTable();
            workbook.Worksheets
                .Add(dataToSave, "entities");
            workbook.SaveAs(excelStream);
        }

        excelStream.Seek(0, SeekOrigin.Begin);

        return excelStream;
    }
    public Stream ExportDataToJson()
    {
        var jsonStream = new MemoryStream();

        var writer = new StreamWriter(jsonStream, System.Text.Encoding.UTF8);
        writer.Write(JsonConvert.SerializeObject(GetEntities()));
        writer.Flush();

        jsonStream.Seek(0, SeekOrigin.Begin);

        return jsonStream;
    }
    public void ImportDataFromJson(IFormFile file)
    {
        var jsonStream = file.OpenReadStream();

        var reader = new StreamReader(jsonStream);
        var jsonData = reader.ReadToEnd();
        foreach (var item in JsonConvert
                     .DeserializeObject<IEnumerable<T>>(jsonData))
        {
            Create(item);
        }
    }

    public void ImportDataFromExcel(IFormFile file)
    {
        var excelStream = file.OpenReadStream();

        var data = new DataTable();

        using var workBook = new XLWorkbook(excelStream);

        var workSheet = workBook.Worksheet(1);

        int rowCounter = 0;
        foreach (IXLRow row in workSheet.Rows())
        {
            if (rowCounter == 0)
            {
                foreach (IXLCell cell in row.Cells())
                {
                    data.Columns.Add(cell.Value.ToString());
                }
                rowCounter++;
            }
            else
            {
                data.Rows.Add();
                int cellCounter = 0;

                foreach (IXLCell cell
                         in row.Cells(row.FirstCellUsed().Address.ColumnNumber,
                             row.
                                 LastCellUsed()
                                 .Address
                                 .ColumnNumber
                         )
                        )
                {
                    data.Rows[data.Rows.Count - 1][cellCounter] = cell.Value.ToString();
                    cellCounter++;
                }
            }
        }

        foreach (var row in data.AsEnumerable())
        {
            Create(row.ToObject<T>());
        }
    }
}