import * as React from "react";
import { UserSignup } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";
import { usePageContent, useSnapshot, useUnhead } from "../../../Shared/Hooks";

export const SignupPage = (): React.ReactElement => {
    useUnhead("SignupPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "templates", "sectionCookiesPrompt", "accountUserSignup"], "SignupPage");

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <UserSignup className="pt-120 pb-240" />
            </main>
        </>
    );
};
