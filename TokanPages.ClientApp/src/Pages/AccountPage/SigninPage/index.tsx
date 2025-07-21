import * as React from "react";
import { UserSignin } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";
import { usePageContent, useSnapshot, useUnhead } from "../../../Shared/Hooks";

export const SigninPage = (): React.ReactElement => {
    useUnhead("SigninPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "templates", "sectionCookiesPrompt", "accountUserSignin"], "SigninPage");

    return (
        <>
            <Navigation backNavigationOnly />
            <main className="pt-6">
                <UserSignin />
            </main>
        </>
    );
};
