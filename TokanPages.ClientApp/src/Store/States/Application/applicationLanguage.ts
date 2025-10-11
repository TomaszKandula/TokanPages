import { ErrorContentDto, LanguageItemDto, PagesModelDto, MetaModelDto, WarningModelDto } from "../../../Api/Models";

export interface ApplicationLanguageState {
    id: string;
    flagImageDir: string;
    flagImageType: string;
    languages: LanguageItemDto[];
    warnings: WarningModelDto[];
    pages: PagesModelDto[];
    meta: MetaModelDto[];
    errorBoundary: ErrorContentDto[];
}
