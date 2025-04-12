import * as React from "react";
import { useSelector } from "react-redux";
import { useHead } from "@unhead/react";
import { UseHeadInput } from "unhead/types";
import { ApplicationState } from "../../../Store/Configuration";
import { CleanupSnapshotMode } from "../../../Shared/Services/SpaCaching";
import Validate from "validate.js";

export const useUnhead = (page?: string): void => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const [links, setLinks] = React.useState<any[]>([]);

    const title = page ?? "software developer";
    const href = `${window.location.origin}${window.location.pathname}`;
    const path = CleanupSnapshotMode(window.location.pathname);
    const url = CleanupSnapshotMode(href);

    React.useEffect(() => {
        if (Validate.isEmpty(language.id)) {
            return;
        }

        const data: any[] = [];
        language?.languages.forEach(item => {
            const href = url.replace(`/${language?.id}`, `/${item.id}`);
            data.push({ rel: "alternate", href: href, hreflang: item.iso });
            if (item.isDefault) {
                data.push({ rel: "alternate", href: url, hreflang: "x-default" });
            }
        });

        data.push({ rel: "canonical", href: url });

        setLinks(data);
    }, [language, window.location.href]);

    const configuration: UseHeadInput = {
        title: "tomkandula",
        titleTemplate: `%s | ${title}`,
        htmlAttrs: {
            lang: language.id,
        },
        base: {
            href: path,
        },
        meta: [
            { property: "og:url", content: url },
            { property: "twitter:url", content: url },
        ],
        link: links
    };

    useHead(configuration);
}
