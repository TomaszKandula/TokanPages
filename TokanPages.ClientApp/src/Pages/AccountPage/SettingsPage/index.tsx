import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { Navigation, Footer } from "../../../Components/Layout";
import { AccessDenied, UserDeactivation, UserInfo, UserPassword, UserRemoval } from "../../../Components/Account";
import Validate from "validate.js";

export const SettingsPage = (): React.ReactElement => {
    const dispatch = useDispatch();

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["navigation", "footer", "templates", "cookiesPrompt", "accountSettings"],
                "SettingsPage"
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
                        <UserInfo />
                        <UserPassword />
                        <UserDeactivation />
                        <UserRemoval />
                    </div>
                )}
            </main>
            <Footer />
        </>
    );
};
