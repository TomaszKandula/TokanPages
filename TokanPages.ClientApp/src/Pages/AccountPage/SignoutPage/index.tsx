import * as React from "react";
import { usePageContent, useUnhead } from "../../../Shared/Hooks";
import { UserSignout } from "../../../Components/Account";
import { Footer, Navigation } from "../../../Components/Layout";

export const SignoutPage = (): React.ReactElement => {
    const heading = useUnhead("SignoutPage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountUserSignout"],
        "SignoutPage"
    );

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <UserSignout />
            </main>
            <Footer />
        </>
    );
};
