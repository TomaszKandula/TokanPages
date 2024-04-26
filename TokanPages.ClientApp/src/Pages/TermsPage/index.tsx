import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

import { ContentNavigationAction, ContentFooterAction, ContentTermsAction, ContentTemplatesAction } from "../../Store/Actions";

export const TermsPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const terms = useSelector((state: ApplicationState) => state.contentTerms);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentTermsAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <DocumentContent content={terms?.content} isLoading={terms?.isLoading} />
            </Container>
            <Footer />
        </>
    );
};
