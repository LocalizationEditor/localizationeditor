﻿using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.Commands.Requests
{
  public interface IUpdateLocalizationStringRequest : IRequest<ILocalizationRow>
  {
    long Id { get; }
    ILocalizationRow LocalizationString { get; }
  }
}