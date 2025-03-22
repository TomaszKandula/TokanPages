import { ErrorContentDto, LanguageItemDto } from "../../../Api/Models";

export interface ApplicationLanguageState {
    id: string;
    languages: LanguageItemDto[];
    errorBoundary: ErrorContentDto[];
}
