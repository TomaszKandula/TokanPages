import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import Validate from "validate.js";

import { AccessDenied, UserInfo, UserPassword, UserRemoval } from "../../Components/Account";

import { ContentNavigationAction, ContentFooterAction, ContentAccountAction, ContentTemplatesAction } from "../../Store/Actions";

export const AccountPage = (): JSX.Element => {
    const dispatch = useDispatch();

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const userStore = useSelector((state: ApplicationState) => state.userDataStore.userData);
    const isAnonymous = Validate.isEmpty(userStore.userId);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentAccountAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />

            {isAnonymous ? (
                <AccessDenied />
            ) : (
                <>
                    <UserInfo />
                    <UserPassword />
                    <UserRemoval />
                </>
            )}

            <Footer />
        </>
    );
};
