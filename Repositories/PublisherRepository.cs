﻿using LibraryWebApi.Entities;
using LibraryWebApi.Models.OptionsModels;
using LibraryWebApi.Resolvers.Interfaces;
using Microsoft.Extensions.Options;

namespace LibraryWebApi.Repositories;

internal sealed class PublisherRepository : BaseRepository<Publishers>
{
    public PublisherRepository(IOptions<DatabaseOptions> options, IDbContextResolver dbContext) : base(options, dbContext)
    {
    }
}