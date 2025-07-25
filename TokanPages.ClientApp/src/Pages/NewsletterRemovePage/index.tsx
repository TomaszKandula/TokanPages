import * as React from "react";
import { useLocation } from "react-router-dom";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { NewsletterRemove } from "../../Components/Newsletter";
import { Navigation } from "../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const NewsletterRemovePage = (): React.ReactElement => {
    useUnhead("NewsletterRemovePage");
    usePageContent(
        ["layoutNavigation", "templates", "sectionCookiesPrompt", "pageNewsletterRemove"],
        "NewsletterRemovePage"
    );

    const queryParam = useQuery();
    const id = queryParam.get("id");

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <NewsletterRemove id={id} />
            </main>
        </>
    );
};
