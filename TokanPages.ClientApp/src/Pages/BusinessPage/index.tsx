import React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { BusinessForm } from "../../Components/Business";
import { Footer, Navigation } from "../../Components/Layout";

export const BusinessPage = () => {
    const heading = useUnhead("BusinessPage");
    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageBusinessForm"],
        "BusinessPage"
    );

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <BusinessForm hasIcon hasShadow />
            </main>
            <Footer />
        </>
    );
};
