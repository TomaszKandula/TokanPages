import React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { BusinessForm } from "../../Components/Business";
import { Navigation } from "../../Components/Layout";

export const BusinessPage = () => {
    useUnhead("BusinessPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "templates", "sectionCookiesPrompt", "pageBusinessForm"], "BusinessPage");

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main className="pt-6">
                <BusinessForm hasCaption={false} hasIcon={true} hasShadow={true} />
            </main>
        </>
    );
};
