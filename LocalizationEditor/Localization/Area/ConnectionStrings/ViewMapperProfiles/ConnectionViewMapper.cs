using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;

namespace Localization.Area.ConnectionStrings.ViewMapperProfiles
{
  internal class ConnectionViewMapper : Profile
  {
    public ConnectionViewMapper()
    {
      CreateMap<IConnection, ConnectionViewModel>()
        .ForMember(destinationMember => destinationMember.ConnectionName,
          sourceOption => sourceOption.MapFrom(option => option.ConnectionName))
        .ForMember(destinationMember => destinationMember.DbName,
          sourceOption => sourceOption.MapFrom(option => option.DbName))
        .ForMember(destinationMember => destinationMember.UserName,
          sourceOption => sourceOption.MapFrom(option => option.UserName))
        .ForMember(destinationMember => destinationMember.Password,
          sourceOption => sourceOption.MapFrom(option => option.Password))
        .ForMember(destinationMember => destinationMember.Server,
          sourceOption => sourceOption.MapFrom(option => option.Server))
        .ForMember(destinationMember => destinationMember.DataBaseType,
          sourceOption => sourceOption.MapFrom(option => option.DataBaseType))
        .ForMember(destinationMember => destinationMember.DataBaseType,
          sourceOption => sourceOption.MapFrom(option => option.DataBaseType))
        .ReverseMap()
        .ConstructUsing((source, ctx) => new ConnectionDto(
          source.Server,
          source.DbName,
          source.UserName,
          source.Password,
          source.DataBaseType,
          source.ConnectionName));
    }
  }
}