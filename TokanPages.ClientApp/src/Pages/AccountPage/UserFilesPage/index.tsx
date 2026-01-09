import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { usePageContent, useUnhead } from "../../../Shared/Hooks";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied } from "../../../Components/Account";
import Validate from "validate.js";

export const UserFilesPage = (): React.ReactElement => {
    const heading = useUnhead("UserFilesPage");
    usePageContent(
        [
            "layoutNavigation",
            "layoutFooter",
            "templates",
            "sectionCookiesPrompt",
            "accountSettings",
            "accountUserFiles",
        ],
        "UserFilesPage"
    );

    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                {isAnonymous ? <AccessDenied /> : <div>User Files</div>}
            </main>
            <Footer />
        </>
    );
};
