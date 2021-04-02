using System;
using AutoMapper;
using LocalizationEditor.Admin.Models;
using LocalizationEditor.Admin.Models.Implementations;
using LocalizationEditor.Web.Controllers.ConnectionStrings;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class UserViewMapper : Profile
  {
    public UserViewMapper()
    {
      CreateMap<IUser, UserView>()
        .ForMember(destinationMember => destinationMember.Id,
          sourceOption => sourceOption.MapFrom(option => option.Id))
        .ForMember(destinationMember => destinationMember.Password,
          sourceOption => sourceOption.MapFrom(option => option.Password))
        .ForMember(destinationMember => destinationMember.UserName,
          sourceOption => sourceOption.MapFrom(option => option.Email))
        .ForMember(destinationMember => destinationMember.Role,
          sourceOption =>
            sourceOption.MapFrom(option => new IdNamePairView
            {
              Id = (long)option.Role,
              Name = Enum.GetName(typeof(RoleType), option.Role)
            }))
        .ReverseMap()
        .ConstructUsing(source => new User(
          source.Id,
          source.UserName,
          source.Password,
          Enum.Parse<RoleType>(source.Role.Name, true)));
    }
  }
}