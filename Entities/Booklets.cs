using LibraryWebApi.Attributes;
using LibraryWebApi.Entities.Interfaces;
using System;

namespace LibraryWebApi.Entities;

[DbTableName(TableName = "Booklets")]
public class Booklets : IEntityId, IEntityDateInfo
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public int BookCount { get; set; }
    public double Price { get; set; }
    public string WikiLink { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}