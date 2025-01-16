import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied } from "../../../Components/Account";
import Validate from "validate.js";
import { UserNotes } from "Components/Account/UserAccount";

export const UserNotesPage = (): React.ReactElement => {
    const dispatch = useDispatch();

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "footer", "templates", "account"], "UserNotesPage"));
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

            <Footer />
        </>
    );
};
