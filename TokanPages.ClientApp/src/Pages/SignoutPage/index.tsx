import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UserSignout } from "../../Components/Account";

import { ContentNavigationAction, ContentFooterAction, ContentUserSignoutAction } from "../../Store/Actions";

export const SignoutPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUserSignoutAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <UserSignout />
            </Container>
            <Footer />
        </>
    );
};
