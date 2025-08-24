import * as React from "react";
import { usePageContent, useQuery, useUnhead } from "../../../Shared/Hooks";
import { AccountActivate } from "../../../Components/Account";
import { Footer, Navigation } from "../../../Components/Layout";

export const ActivationPage = (): React.ReactElement => {
    useUnhead("ActivationPage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountSettings", "accountActivate"],
        "ActivationPage"
    );

    const queryParam = useQuery();
    const id = queryParam.get("id") ?? "";
    const type = queryParam.get("type") ?? "";

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <AccountActivate id={id} type={type} />
            </main>
            <Footer />
        </>
    );
};
