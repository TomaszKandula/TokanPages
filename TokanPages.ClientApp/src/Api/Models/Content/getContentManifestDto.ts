import { ILanguageItem } from "./Items/languageItemDto";

export interface IGetContentManifestDto
{
    version: string;
    created: string;
    updated: string;
    languages: ILanguageItem[];
}
