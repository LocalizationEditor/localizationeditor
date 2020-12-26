import {LocalizationStringDto} from "./localization-string-dto";

export interface LocalizationDataRowServerDto {
    id: number;
    group: string;
    key: string;
    localizations: LocalizationStringDto[];
}
