using LibraryWebApi.Attributes;
using LibraryWebApi.Entities.Interfaces;
using System;

namespace LibraryWebApi.Entities;

[DbTableName(TableName = "ReaderBook")]
public class ReaderBook : IEntityId, IEntityDateInfo
{
    public int Id { get; set; }
    public int ReaderId { get; set; }
    public int BookId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}