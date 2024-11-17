import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { Box } from "@material-ui/core";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { PdfViewer } from "../../Components/PdfViewer";
import { Navigation } from "../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const PdfViewerPage = () => {
    const queryParam = useQuery();
    const dispatch = useDispatch();

    const name = queryParam.get("name") ?? "";
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "footer", "templates"], "PdfViewer"));
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <Box mt={6}>
                <PdfViewer pdfFile={name} background={{ backgroundColor: "#FCFCFC" }} />
            </Box>
        </>
    );
};
