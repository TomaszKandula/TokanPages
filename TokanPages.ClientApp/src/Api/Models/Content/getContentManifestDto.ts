import { ErrorContentDto } from "./Items/errorContentDto";
import { LanguageItemDto } from "./Items/languageItemDto";
import { MetaModelDto } from "./Items/metaModelDto";
import { PagesModelDto } from "./Items/pagesModelDto";

export interface GetContentManifestDto {
    version: string;
    created: string;
    updated: string;
    default: string;
    languages: LanguageItemDto[];
    pages: PagesModelDto[];
    meta: MetaModelDto[];
    errorBoundary: ErrorContentDto[];
}
