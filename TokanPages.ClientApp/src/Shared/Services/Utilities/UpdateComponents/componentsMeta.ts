import { META_ATTRIBUTE, QUERY_SELECTOR } from "../../../../Shared/constants";

export const GetComponentMeta = (): string => {
    const meta = document.querySelector(QUERY_SELECTOR);
    return meta?.getAttribute(META_ATTRIBUTE) ?? "";
};

export const SetComponentMeta = (pageName?: string, languageId?: string): void => {
    const meta = document.querySelector(QUERY_SELECTOR);
    meta?.setAttribute(META_ATTRIBUTE, `${pageName}-${languageId}`.toLowerCase());
};

export const HasPageContentLoaded = (pageName?: string): boolean => {
    if (!pageName) {
        return false;
    }

    const meta = GetComponentMeta();
    return meta.includes(pageName);
};
