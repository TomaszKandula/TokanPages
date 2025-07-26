import * as React from "react";
import { usePageContent, useUnhead } from "../../../Shared/Hooks";
import { UserSignout } from "../../../Components/Account";
import { Footer, Navigation } from "../../../Components/Layout";

export const SignoutPage = (): React.ReactElement => {
    useUnhead("SignoutPage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountUserSignout"],
        "SignoutPage"
    );

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <UserSignout />
            </main>
            <Footer />
        </>
    );
};
