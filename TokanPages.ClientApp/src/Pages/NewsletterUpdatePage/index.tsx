import * as React from "react";
import { useLocation } from "react-router-dom";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { NewsletterUpdate } from "../../Components/NewsletterUpdate";
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
            <Navigation backNavigationOnly={true} />
            <main>
                <NewsletterUpdate id={id} className="pt-120 pb-240" />
            </main>
        </>
    );
};
