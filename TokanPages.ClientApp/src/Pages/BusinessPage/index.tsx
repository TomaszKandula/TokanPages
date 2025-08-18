import React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { BusinessForm } from "../../Components/Business";
import { Footer, Navigation } from "../../Components/Layout";

export const BusinessPage = () => {
    useUnhead("BusinessPage");
    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageBusinessForm"],
        "BusinessPage"
    );

    return (
        <>
            <Navigation />
            <main>
                <BusinessForm hasIcon hasShadow />
            </main>
            <Footer />
        </>
    );
};
