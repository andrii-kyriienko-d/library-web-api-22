using System.Collections.Generic;

namespace LibraryWebApi.Models.RequestModels;

public class DeleteModel
{
    public IEnumerable<int> Ids { get; set; }
}