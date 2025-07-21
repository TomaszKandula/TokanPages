import * as React from "react";
import { useLocation } from "react-router-dom";
import { usePageContent, useUnhead } from "../../../Shared/Hooks";
import { AccountActivate } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const ActivationPage = (): React.ReactElement => {
    useUnhead("ActivationPage");
    usePageContent(
        ["layoutNavigation", "templates", "sectionCookiesPrompt", "accountSettings", "accountActivate"],
        "ActivationPage"
    );

    const queryParam = useQuery();
    const id = queryParam.get("id") ?? "";
    const type = queryParam.get("type") ?? "";

    return (
        <>
            <Navigation backNavigationOnly />
            <main className="pt-6">
                <AccountActivate id={id} type={type} />
            </main>
        </>
    );
};
