import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { Box, Container } from "@material-ui/core";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { PdfViewer } from "../../Components/PdfViewer";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const PdfViewerPage = () => {
    const queryParam = useQuery();
    const dispatch = useDispatch();

    const name = queryParam.get("name") ?? "";
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <Box mt={8}>
                    <PdfViewer pdfFile={name} />
                </Box>
            </Container>
            <Footer />
        </>
    );
};
