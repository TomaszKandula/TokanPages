import * as React from "react";
import { useSelector } from "react-redux";
import { useHead } from "@unhead/react";
import { UseHeadInput } from "unhead/types";
import { MetaModelDto, PageModelDto, PagesModelDto } from "../../../Api/Models";
import { ApplicationLanguageState } from "../../../Store/States";
import { ApplicationState } from "../../../Store/Configuration";
import { CleanupSnapshotMode } from "../../../Shared/Services/SpaCaching";
import Validate from "validate.js";

const getTitle = (pages: PagesModelDto[], pageId: string, languageId: string): string => {
    const data = pages.filter((value: PagesModelDto) => value.language === languageId ? value : undefined)[0];
    const result = data.pages.filter((value: PageModelDto) => value.page === pageId ? value : undefined)[0].title;
    return result;
}

const getMeta = (meta: MetaModelDto[], languageId: string): MetaModelDto => {
    return meta.filter((value: MetaModelDto) => value.language === languageId ? value : undefined)[0];
}

const getLinks = (language: ApplicationLanguageState, url: string): any[] => {
    const data: any[] = [];
    language?.languages.forEach(item => {
        const href = url.replace(`/${language?.id}`, `/${item.id}`);
        data.push({ rel: "alternate", href: href, hreflang: item.iso });
        if (item.isDefault) {
            data.push({ rel: "alternate", href: url, hreflang: "x-default" });
        }
    });

    data.push({ rel: "canonical", href: url });

    return data;
}

export const useUnhead = (pageId: string): void => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    const [links, setLinks] = React.useState<any[]>([]);
    const [title, setTitle] = React.useState<string>("");
    const [meta, setMeta] = React.useState<MetaModelDto | undefined>(undefined);

    const href = `${window.location.origin}${window.location.pathname}`;
    const path = CleanupSnapshotMode(window.location.pathname);
    const url = CleanupSnapshotMode(href);

    React.useEffect(() => {
        if (Validate.isEmpty(language?.id)) {
            return;
        }

        const links = getLinks(language, url);
        const title = getTitle(language?.pages, pageId, language?.id);
        const meta = getMeta(language?.meta, language?.id);

        setLinks(links);
        setTitle(title);
        setMeta(meta);

    }, [language, window.location.href]);

    const configuration: UseHeadInput = {
        title: "tomkandula",
        titleTemplate: `%s | ${title}`,
        htmlAttrs: {
            lang: language?.id ?? "",
        },
        base: {
            href: path,
        },
        meta: [
            { name: "description", content: meta?.description ?? "" },
            { property: "og:title", content: meta?.facebook?.title ?? "" },
            { property: "og:description", content: meta?.facebook?.description ?? "" },
            { property: "og:image:alt", content: meta?.facebook?.imageAlt ?? "" },
            { property: "og:url", content: url },
            { property: "twitter:title", content: meta?.twitter?.title ?? "" },
            { property: "twitter:description", content: meta?.twitter?.description ?? "" },
            { property: "twitter:url", content: url },
        ],
        link: links
    };

    useHead(configuration);
}
