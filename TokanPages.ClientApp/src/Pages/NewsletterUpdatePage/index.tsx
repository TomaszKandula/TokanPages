import * as React from "react";
import { usePageContent, useQuery, useUnhead } from "../../Shared/Hooks";
import { NewsletterUpdate } from "../../Components/Newsletter";
import { Footer, Navigation } from "../../Components/Layout";

export const NewsletterUpdatePage = (): React.ReactElement => {
    const heading = useUnhead("NewsletterUpdatePage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageNewsletterUpdate"],
        "NewsletterUpdatePage"
    );

    const queryParam = useQuery();
    const id = queryParam.get("id") as string;

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <NewsletterUpdate id={id} />
            </main>
            <Footer />
        </>
    );
};
