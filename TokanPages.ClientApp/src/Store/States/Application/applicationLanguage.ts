import { ErrorContentDto, LanguageItemDto, PagesModelDto, MetaModelDto } from "../../../Api/Models";

export interface ApplicationLanguageState {
    id: string;
    languages: LanguageItemDto[];
    pages: PagesModelDto[];
    meta: MetaModelDto[];
    errorBoundary: ErrorContentDto[];
}
