﻿using LibraryWebApi.Entities;
using LibraryWebApi.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryWebApi.Services;

internal sealed class PublishersService : BaseService<Publishers>
{
    public PublishersService(IRepository<Publishers> publisherRepository, IMemoryCache cache) 
        : base(publisherRepository, cache)
    {
    }
}