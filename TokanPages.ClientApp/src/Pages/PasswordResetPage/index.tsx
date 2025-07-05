import * as React from "react";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { PasswordReset } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";

export const PasswordResetPage = (): React.ReactElement => {
    useUnhead("PasswordResetPage");
    usePageContent(["layoutNavigation", "templates", "sectionCookiesPrompt", "pagePasswordReset"], "PasswordResetPage");

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main className="pt-6">
                <PasswordReset />
            </main>
        </>
    );
};
