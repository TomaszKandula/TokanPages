import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UpdatePassword } from "../../Components/Account";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentUpdatePasswordAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const PasswordUpdatePage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUpdatePasswordAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <UpdatePassword />
            </Container>
            <Footer />
        </>
    );
};
