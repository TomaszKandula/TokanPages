import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { usePageContent, useUnhead } from "../../../Shared/Hooks";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied, UserDeactivation, UserInfo, UserPassword, UserRemoval } from "../../../Components/Account";
import Validate from "validate.js";

export const SettingsPage = (): React.ReactElement => {
    useUnhead("SettingsPage");
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountSettings"],
        "SettingsPage"
    );

    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    return (
        <>
            <Navigation backNavigationOnly={isAnonymous} />
            <main>
                {isAnonymous ? (
                    <AccessDenied />
                ) : (
                    <>
                        <UserInfo className="pt-6 pb-4" />
                        <UserPassword className="py-4" />
                        <UserDeactivation className="py-4" />
                        <UserRemoval className="pt-4 pb-6" />
                    </>
                )}
            </main>
            <Footer />
        </>
    );
};
