import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentShowcaseAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const ShowcasePage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const showcase = useSelector((state: ApplicationState) => state.contentShowcase);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentShowcaseAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <DocumentContent content={showcase?.content} isLoading={showcase?.isLoading} />
            </Container>
            <Footer />
        </>
    );
};
