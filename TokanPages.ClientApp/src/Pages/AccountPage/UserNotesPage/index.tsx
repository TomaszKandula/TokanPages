import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { useUnhead } from "../../../Shared/Hooks";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied, UserNotes } from "../../../Components/Account";
import Validate from "validate.js";

export const UserNotesPage = (): React.ReactElement => {
    useUnhead("user notes");

    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "accountSettings", "accountUserNotes"],
                "UserNotesPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <main>
                {isAnonymous ? (
                    <AccessDenied />
                ) : (
                    <div className="pb-40">
                        <UserNotes />
                    </div>
                )}
            </main>
            <Footer />
        </>
    );
};
