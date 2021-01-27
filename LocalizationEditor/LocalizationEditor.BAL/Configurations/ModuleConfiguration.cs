using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings;
using LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Services.LocalizationStrings;
using LocalizationEditor.BAL.Services.LocalizationStrings.Implementations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace LocalizationEditor.BAL.Configurations
{
  public class ModuleConfiguration : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<LocalizationStringService>()
        .As<ILocalizationStringService>()
        .InstancePerLifetimeScope();



    }
  }
}