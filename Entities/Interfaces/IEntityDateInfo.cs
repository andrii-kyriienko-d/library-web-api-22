using System;

namespace LibraryWebApi.Entities.Interfaces;

public interface IEntityDateInfo
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}