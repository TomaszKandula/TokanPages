import * as React from "react";
import { usePageContent, useUnhead } from "../../../Shared/Hooks";
import { UserSignout } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";

export const SignoutPage = (): React.ReactElement => {
    useUnhead("SignoutPage");
    usePageContent(["layoutNavigation", "templates", "sectionCookiesPrompt", "accountUserSignout"], "SignoutPage");

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <UserSignout className="pt-120 pb-240" />
            </main>
        </>
    );
};
