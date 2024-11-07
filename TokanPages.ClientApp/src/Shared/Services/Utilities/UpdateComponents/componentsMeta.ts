import { META_ATTRIBUTE, QUERY_SELECTOR } from "../../../../Shared/constants";

export const GetComponentMeta = (): string => {
    const meta = document.querySelector(QUERY_SELECTOR);
    return meta?.getAttribute(META_ATTRIBUTE) ?? "";
};

export const SetComponentMeta = (pageName?: string, languageId?: string): void => {
    const meta = document.querySelector(QUERY_SELECTOR);
    meta?.setAttribute(META_ATTRIBUTE, `last-cache: ${pageName}; language: ${languageId};`.toLowerCase());
};

export const HasPageContentLoaded = (pageName?: string): boolean => {
    if (!pageName) {
        return false;
    }

    const page = pageName.toLocaleLowerCase();
    const meta = GetComponentMeta().toLocaleLowerCase();
    return meta.includes(page);
};
