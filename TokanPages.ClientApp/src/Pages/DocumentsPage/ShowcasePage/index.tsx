import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../../Store/Configuration";
import { Navigation, Footer } from "../../../Components/Layout";
import { DocumentContent } from "../../../Components/Document";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentDocumentAction,
    ContentTemplatesAction,
} from "../../../Store/Actions";

export const ShowcasePage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const document = useSelector((state: ApplicationState) => state.contentDocument);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentDocumentAction.getShowcase());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    const isLoading = document?.contentShowcase?.isLoading ?? false;
    const items = document?.contentShowcase?.content.items ?? [];

    return (
        <>
            <Navigation />
            <Container>
                <DocumentContent isLoading={isLoading} items={items} />
            </Container>
            <Footer />
        </>
    );
};
