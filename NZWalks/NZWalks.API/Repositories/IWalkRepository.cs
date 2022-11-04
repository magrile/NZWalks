﻿using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        // Obtener todos los walks

        public Task<IEnumerable<Walk>> GetAllWalksAsync();

        // Obtener un paseo
        public Task<Walk> GetWalkById(Guid walkId);

        // Añadir un paseo

        public Task<Walk> AddNewWalk(Walk walk);


        
    }
}
