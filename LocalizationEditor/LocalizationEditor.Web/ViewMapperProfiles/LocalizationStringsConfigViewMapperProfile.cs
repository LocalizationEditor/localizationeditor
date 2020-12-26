﻿using AutoMapper;
using Localization.ViewModels.LocalizationStrings;
using System.Collections.Generic;

namespace Localization.ViewMapperProfiles
{
  internal class LocalizationStringsConfigViewMapperProfile : Profile
  {
    public LocalizationStringsConfigViewMapperProfile()
    {
      CreateMap<IEnumerable<string>, LocalizationStringsConfigView>()
        .ForMember(destinationMember => destinationMember.Locales,
          memberOption => memberOption.MapFrom(i => i));
    }
  }
}