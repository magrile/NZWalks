﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class WalksProfile: Profile
    {

        public WalksProfile()
        {
            CreateMap<Walk, WalkDto>()
                .ReverseMap();

            CreateMap<WalkDifficulty, WalkDifficultyDto>()
                .ReverseMap();
        }
    }
}
