import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Container } from "@material-ui/core";
import { API_BASE_URI } from "../../Api/Request";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { PdfViewer } from "../../Components/PdfViewer";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentTemplatesAction,
    ContentHeaderAction,
} from "../../Store/Actions";

export const PdfViewerPage = () => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const header = useSelector((state: ApplicationState) => state.contentHeader);
    const href = header?.content?.resume?.href;
    const url = API_BASE_URI + href;

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentHeaderAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <Box mt={8}>
                    <PdfViewer pdfFile={url} />
                </Box>
            </Container>
            <Footer />
        </>
    );
};
