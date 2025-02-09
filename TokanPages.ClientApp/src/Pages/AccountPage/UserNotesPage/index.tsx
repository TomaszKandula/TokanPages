import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied, UserNotes } from "../../../Components/Account";
import { Cookies } from "../../../Components/Cookies";
import Validate from "validate.js";

export const UserNotesPage = (): React.ReactElement => {
    const dispatch = useDispatch();

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request([
            "navigation", 
            "footer", 
            "templates", 
            "cookiesPrompt", 
            "accountUserNotes"
        ], "UserNotesPage"));
    }, [language?.id]);

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

            <Cookies />
            <Footer />
        </>
    );
};
