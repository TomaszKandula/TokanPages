import { META_ATTRIBUTE, QUERY_SELECTOR } from "../../../../Shared/constants";

export const GetComponentMeta = (): string => {
    const meta = document.querySelector(QUERY_SELECTOR);
    return meta?.getAttribute(META_ATTRIBUTE) ?? "";
};

export const SetComponentMeta = (pageName?: string, languageId?: string): void => {
    const meta = document.querySelector(QUERY_SELECTOR);
    meta?.setAttribute(META_ATTRIBUTE, `last-cache: ${pageName}; language: ${languageId};`.toLowerCase());
};

export const HasPageContentLoaded = (pageName?: string, languageId?: string): boolean => {
    if (!pageName || !languageId) {
        return false;
    }

    const page = pageName.toLocaleLowerCase();
    const lang = languageId.toLocaleLowerCase();
    const meta = GetComponentMeta().toLocaleLowerCase();

    return meta.includes(page) && meta.includes(lang);
};
