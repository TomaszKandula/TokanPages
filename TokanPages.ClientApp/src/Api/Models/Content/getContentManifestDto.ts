import { ErrorContentDto } from "./Items/errorContentDto";
import { LanguageItemDto } from "./Items/languageItemDto";
import { MetaModelDto } from "./Items/metaModelDto";
import { PagesModelDto } from "./Items/pagesModelDto";
import { WarningModelDto } from "./Items/warningModelDto";

export interface GetContentManifestDto {
    version: string;
    created: string;
    updated: string;
    default: string;
    flagImageType: string;
    languages: LanguageItemDto[];
    warnings: WarningModelDto[];
    pages: PagesModelDto[];
    meta: MetaModelDto[];
    errorBoundary: ErrorContentDto[];
}
