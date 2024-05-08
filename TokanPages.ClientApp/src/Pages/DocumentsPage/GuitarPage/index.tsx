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

export const GuitarPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const document = useSelector((state: ApplicationState) => state.contentDocument);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentDocumentAction.getGuitar());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    const isLoading = document?.contentGuitar?.isLoading ?? false;
    const items = document?.contentGuitar?.content.items ?? [];

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
