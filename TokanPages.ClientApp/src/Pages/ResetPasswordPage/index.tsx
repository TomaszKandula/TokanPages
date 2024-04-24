import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { ResetPassword } from "../../Components/Account";

import { ContentNavigationAction, ContentFooterAction, ContentResetPasswordAction } from "../../Store/Actions";

export const ResetPasswordPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentResetPasswordAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <ResetPassword />
            </Container>
            <Footer />
        </>
    );
};
