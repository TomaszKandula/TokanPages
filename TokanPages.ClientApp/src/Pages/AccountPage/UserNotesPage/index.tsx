import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { usePageContent, useUnhead } from "../../../Shared/Hooks";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied, UserNotes } from "../../../Components/Account";
import Validate from "validate.js";

export const UserNotesPage = (): React.ReactElement => {
    useUnhead("UserNotesPage");
    usePageContent(
        [
            "layoutNavigation",
            "layoutFooter",
            "templates",
            "sectionCookiesPrompt",
            "accountSettings",
            "accountUserNotes",
        ],
        "UserNotesPage"
    );

    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    return (
        <>
            <Navigation />
            <main className="pt-6">
                {isAnonymous ? (
                    <AccessDenied />
                ) : (
                    <UserNotes />
                )}
            </main>
            <Footer />
        </>
    );
};
