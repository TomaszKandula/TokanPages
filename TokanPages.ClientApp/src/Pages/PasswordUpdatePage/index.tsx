import * as React from "react";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { PasswordUpdate } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";

export const PasswordUpdatePage = (): React.ReactElement => {
    useUnhead("PasswordUpdatePage");
    usePageContent(
        ["layoutNavigation", "templates", "sectionCookiesPrompt", "pagePasswordUpdate"],
        "PasswordUpdatePage"
    );

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <PasswordUpdate className="pt-120 pb-240" />
            </main>
        </>
    );
};
