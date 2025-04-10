import { useSelector } from "react-redux";
import { useHead } from "@unhead/react";
import { ApplicationState } from "../../../Store/Configuration";
import { CleanupSnapshotMode } from "../../../Shared/Services/SpaCaching";

export const useUnhead = (page?: string): void => {
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;

    const url = window.location.href;
    const pathname = window.location.pathname;
    const cleanupUrl = CleanupSnapshotMode(url);
    const title = page ?? "software developer";

    useHead({
        title: "tomkandula",
        titleTemplate: `%s | ${title}`,
        htmlAttrs: {
            lang: language.id,
        },
        base: {
            href: pathname,
        },
        meta: [
            { property: "og:url", content: url },
            { property: "twitter:url", content: url },
        ],
        link: [
            { rel: "canonical", href: cleanupUrl },
        ]
    });
}
