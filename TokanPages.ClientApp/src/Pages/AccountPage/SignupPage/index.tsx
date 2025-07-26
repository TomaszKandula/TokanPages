import * as React from "react";
import { UserSignup } from "../../../Components/Account";
import { Footer, Navigation } from "../../../Components/Layout";
import { usePageContent, useSnapshot, useUnhead } from "../../../Shared/Hooks";

export const SignupPage = (): React.ReactElement => {
    useUnhead("SignupPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountUserSignup"], "SignupPage");

    return (
        <>
            <Navigation />
            <main>
                <UserSignup />
            </main>
            <Footer />
        </>
    );
};
