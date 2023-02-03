using LibraryWebApi.Attributes;
using LibraryWebApi.Entities.Interfaces;
using System;

namespace LibraryWebApi.Entities;

[DbTableName(TableName = "Publishers")]
public sealed class Publishers : IEntityId, IEntityDateInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CompanyUCode { get; set; }
    public string Location { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}