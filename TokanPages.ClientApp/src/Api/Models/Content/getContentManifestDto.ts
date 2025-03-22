import { ErrorContentDto } from "./Items/errorContentDto";
import { LanguageItemDto } from "./Items/languageItemDto";

export interface GetContentManifestDto {
    version: string;
    created: string;
    updated: string;
    default: string;
    languages: LanguageItemDto[];
    errorBoundary: ErrorContentDto[];
}
