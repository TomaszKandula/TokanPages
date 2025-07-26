import * as React from "react";
import { UserSignin } from "../../../Components/Account";
import { Footer, Navigation } from "../../../Components/Layout";
import { usePageContent, useSnapshot, useUnhead } from "../../../Shared/Hooks";

export const SigninPage = (): React.ReactElement => {
    useUnhead("SigninPage");
    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountUserSignin"],
        "SigninPage"
    );

    return (
        <>
            <Navigation />
            <main>
                <UserSignin />
            </main>
            <Footer />
        </>
    );
};
