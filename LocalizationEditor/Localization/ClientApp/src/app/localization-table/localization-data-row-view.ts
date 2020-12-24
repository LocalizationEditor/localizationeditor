export interface LocalizationDataRowView {
  id: number;
  group: string;
  key: string;
}

export interface LocalizationDataRowServerDto {
  id: number;
  group: string;
  key: string;
  localizations: LocalizationStringDto[];
}

export interface LocalizationDataRowsServerDto {
  localizationStrings : LocalizationDataRowServerDto[]
}

export interface LocalizationStringDto {
  locale: string;
  value: string;
}
