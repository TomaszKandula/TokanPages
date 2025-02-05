import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied, UserDeactivation, UserInfo, UserPassword, UserRemoval } from "../../../Components/Account";
import Validate from "validate.js";

const baseComponents = ["navigation", "footer", "templates"];
const settingsComponents = ["navigation", "footer", "templates", "accountSettings"];

export const SettingsPage = (): React.ReactElement => {
    const dispatch = useDispatch();

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    React.useEffect(() => {
        if (isAnonymous) {
            dispatch(ContentPageDataAction.request(baseComponents, "SettingsPage"));
            return;
        }

        dispatch(ContentPageDataAction.request(settingsComponents, "SettingsPage"));

    }, [language?.id, isAnonymous]);

    return (
        <>
            <Navigation />

            {isAnonymous ? (
                <AccessDenied />
            ) : (
                <div style={{ paddingBottom: 40 }}>
                    <UserInfo />
                    <UserPassword />
                    <UserDeactivation />
                    <UserRemoval />
                </div>
            )}

            <Footer />
        </>
    );
};
