import {LocalizationStringDto} from "./localization-string-dto";
import {LocalizationGroup} from "./localization-data-row-view";

export interface LocalizationDataRowServerDto {
    id: number;
    group: LocalizationGroup;
    key: string;
    localizations: LocalizationStringDto[];
}
