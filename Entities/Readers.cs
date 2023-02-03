using LibraryWebApi.Attributes;
using LibraryWebApi.Entities.Interfaces;
using System;

namespace LibraryWebApi.Entities;

[DbTableName(TableName = "Readers")]
public sealed class Readers : IEntityId, IEntityDateInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string LibraryCode { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}