import * as React from "react";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { PasswordReset } from "../../Components/Account";
import { Footer, Navigation } from "../../Components/Layout";

export const PasswordResetPage = (): React.ReactElement => {
    const heading = useUnhead("PasswordResetPage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pagePasswordReset"],
        "PasswordResetPage"
    );

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <PasswordReset />
            </main>
            <Footer />
        </>
    );
};
