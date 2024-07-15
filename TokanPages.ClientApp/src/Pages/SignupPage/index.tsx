import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { CustomBreadcrumb } from "../../Shared/Components";
import { Navigation, Footer } from "../../Components/Layout";
import { UserSignup } from "../../Components/Account";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentUserSignupAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const SignupPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUserSignupAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb mt={12} mb={2} mr={5} ml={5} mtDivider={4} mbDivider={4} />
            <UserSignup />
            <Footer />
        </>
    );
};
