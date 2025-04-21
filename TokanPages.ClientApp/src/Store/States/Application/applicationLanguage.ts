import { ErrorContentDto, LanguageItemDto, PagesModelDto, MetaModelDto, WarningModelDto } from "../../../Api/Models";

export interface ApplicationLanguageState {
    id: string;
    languages: LanguageItemDto[];
    warnings: WarningModelDto[];
    pages: PagesModelDto[];
    meta: MetaModelDto[];
    errorBoundary: ErrorContentDto[];
}
