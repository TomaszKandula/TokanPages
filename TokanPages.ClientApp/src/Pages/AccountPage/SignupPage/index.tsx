import * as React from "react";
import { UserSignup } from "../../../Components/Account";
import { Footer, Navigation } from "../../../Components/Layout";
import { usePageContent, useSnapshot, useUnhead } from "../../../Shared/Hooks";

export const SignupPage = (): React.ReactElement => {
    const heading = useUnhead("SignupPage");
    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountUserSignup"],
        "SignupPage"
    );

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <UserSignup />
            </main>
            <Footer />
        </>
    );
};
