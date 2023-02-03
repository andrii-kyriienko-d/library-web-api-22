using LibraryWebApi.Attributes;
using LibraryWebApi.Entities.Interfaces;
using System;

namespace LibraryWebApi.Entities;

[DbTableName(TableName ="Books")]
public sealed class Books : IEntityId, IEntityDateInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Pages { get; set; }
    public string Genre { get; set; }
    public int BookletId { get; set; }
    public int PublisherId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}