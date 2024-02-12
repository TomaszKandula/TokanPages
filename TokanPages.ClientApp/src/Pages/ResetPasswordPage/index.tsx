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

    const navigation = useSelector((state: ApplicationState) => state.contentNavigation);
    const footer = useSelector((state: ApplicationState) => state.contentFooter);
    const reset = useSelector((state: ApplicationState) => state.contentResetPassword);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentResetPasswordAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <ResetPassword content={reset?.content} isLoading={reset?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
};
