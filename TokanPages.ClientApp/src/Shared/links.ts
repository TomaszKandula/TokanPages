import { ApplicationLanguageState } from "../Store/States";
import { CleanupSnapshotMode } from "./Services/SpaCaching";
import { LINK_HREF_ATTRIBUTE, LINK_HREFLANG_ATTRIBUTE, LINK_QUERY_SELECTOR, LINK_REL_ATTRIBUTE } from "./constants";

export const CreateCanonicalLink = (): void => {
    const url = CleanupSnapshotMode(window.location.href);
    const link = document.querySelector(LINK_QUERY_SELECTOR);
    if (link === null) {
        let newlink = document.createElement("link");
        newlink.setAttribute(LINK_REL_ATTRIBUTE, "canonical");
        newlink.setAttribute(LINK_HREF_ATTRIBUTE, url);
        document.head.appendChild(newlink);
    } else {
        link.setAttribute(LINK_HREF_ATTRIBUTE, url);
    }
};

export const CreateAlternateLink = (href: string, hreflang: string): void => {
    const url = CleanupSnapshotMode(href);
    const link = document.querySelector(`link[hreflang="${hreflang}"]`);
    if (link === null) {
        let element = document.createElement("link");
        element.setAttribute(LINK_REL_ATTRIBUTE, "alternate");
        element.setAttribute(LINK_HREFLANG_ATTRIBUTE, hreflang);
        element.setAttribute(LINK_HREF_ATTRIBUTE, url);
        document.head.appendChild(element);
    } else {
        link.setAttribute(LINK_HREFLANG_ATTRIBUTE, hreflang);
        link.setAttribute(LINK_HREF_ATTRIBUTE, url);
    }
};

export const CreateAlternateLinks = (language: ApplicationLanguageState): void => {
    language?.languages.forEach(item => {
        const url = window.location.href.replace(`/${language?.id}`, `/${item.id}`);
        CreateAlternateLink(url, item.iso);
        if (item.isDefault) {
            CreateAlternateLink(url, "x-default");
        }
    });
};
