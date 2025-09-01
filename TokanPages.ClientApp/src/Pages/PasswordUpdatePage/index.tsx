import * as React from "react";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { PasswordUpdate } from "../../Components/Account";
import { Footer, Navigation } from "../../Components/Layout";

export const PasswordUpdatePage = (): React.ReactElement => {
    const heading = useUnhead("PasswordUpdatePage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pagePasswordUpdate"],
        "PasswordUpdatePage"
    );

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <PasswordUpdate />
            </main>
            <Footer />
        </>
    );
};
