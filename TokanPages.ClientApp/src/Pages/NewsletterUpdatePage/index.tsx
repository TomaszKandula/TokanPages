import * as React from "react";
import { useLocation } from "react-router-dom";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { NewsletterUpdate } from "../../Components/Newsletter";
import { Navigation } from "../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const NewsletterUpdatePage = (): React.ReactElement => {
    useUnhead("NewsletterUpdatePage");
    usePageContent(
        ["layoutNavigation", "templates", "sectionCookiesPrompt", "pageNewsletterUpdate"],
        "NewsletterUpdatePage"
    );

    const queryParam = useQuery();
    const id = queryParam.get("id") as string;

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <NewsletterUpdate id={id} />
            </main>
        </>
    );
};
