using System;
using AutoMapper;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.Web.ViewModels.ConnectionStrings;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  internal class ConnectionViewMapper : Profile
  {
    public ConnectionViewMapper()
    {
      CreateMap<IConnection, ConnectionViewModel>()
        .ForMember(destinationMember => destinationMember.Id,
          sourceOption => sourceOption.MapFrom(option => option.Id))
        .ForMember(destinationMember => destinationMember.ConnectionName,
          sourceOption => sourceOption.MapFrom(option => option.ConnectionName))
        .ForMember(destinationMember => destinationMember.DbName,
          sourceOption => sourceOption.MapFrom(option => option.DbName))
        .ForMember(destinationMember => destinationMember.UserName,
          sourceOption => sourceOption.MapFrom(option => option.UserName))
        .ForMember(destinationMember => destinationMember.Password,
          sourceOption => sourceOption.MapFrom(option => option.Password))
        .ForMember(destinationMember => destinationMember.ServerName,
          sourceOption => sourceOption.MapFrom(option => option.Server))
        .ForMember(destinationMember => destinationMember.ForAll,
          sourceOption => sourceOption.MapFrom(option => option.ForAll))
        .ForMember(destinationMember => destinationMember.DbType,
          sourceOption =>
            sourceOption.MapFrom(option => new ConnectionDbTypeViewModel
            {
              Id = (long)option.DataBaseType,
              Name = Enum.GetName(typeof(DbType), option.DataBaseType)
            }))
        .ReverseMap()
        .ConstructUsing(source => new ConnectionDto(
          source.Id,
          source.ServerName,
          source.DbName,
          source.UserName,
          source.Password,
          Enum.Parse<DbType>(source.DbType.Name, true),
          source.ConnectionName,
          source.ForAll));
    }
  }
}