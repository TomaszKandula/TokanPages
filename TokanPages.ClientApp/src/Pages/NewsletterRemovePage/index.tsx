import * as React from "react";
import { useLocation } from "react-router-dom";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { NewsletterRemove } from "../../Components/NewsletterRemove";
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
            <Navigation backNavigationOnly={true} />
            <main>
                <NewsletterRemove id={id} className="pt-120 pb-240" />
            </main>
        </>
    );
};
