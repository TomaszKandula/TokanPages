import * as React from "react";
import { usePageContent, useQuery, useUnhead } from "../../Shared/Hooks";
import { NewsletterRemove } from "../../Components/Newsletter";
import { Footer, Navigation } from "../../Components/Layout";

export const NewsletterRemovePage = (): React.ReactElement => {
    useUnhead("NewsletterRemovePage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageNewsletterRemove"],
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
            <Footer />
        </>
    );
};
