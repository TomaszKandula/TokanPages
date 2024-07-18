import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../Store/Configuration";
import { PdfViewer } from "../../Components/PdfViewer";
import { BackArrow } from "../../Shared/Components";
import { Colours } from "../../Theme";

import { ContentNavigationAction, ContentFooterAction, ContentTemplatesAction } from "../../Store/Actions";

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
            <BackArrow />
            <PdfViewer pdfFile={name} background={{ backgroundColor: Colours.colours.lightGray3 }} />
        </>
    );
};
