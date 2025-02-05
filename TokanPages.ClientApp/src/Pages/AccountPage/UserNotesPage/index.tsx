import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied, UserNotes } from "../../../Components/Account";
import Validate from "validate.js";

const baseComponents = ["navigation", "footer", "templates"];
const noteComponents = ["navigation", "footer", "templates", "accountUserNotes"];

export const UserNotesPage = (): React.ReactElement => {
    const dispatch = useDispatch();

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    React.useEffect(() => {
        if (isAnonymous) {
            dispatch(ContentPageDataAction.request(baseComponents, "UserNotesPage"));
            return;
        }

        dispatch(ContentPageDataAction.request(noteComponents, "UserNotesPage"));

    }, [language?.id, isAnonymous]);

    return (
        <>
            <Navigation />

            {isAnonymous ? (
                <AccessDenied />
            ) : (
                <div style={{ paddingBottom: 40 }}>
                    <UserNotes />
                </div>
            )}

            <Footer />
        </>
    );
};
