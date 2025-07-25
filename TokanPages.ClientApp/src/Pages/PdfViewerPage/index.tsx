import React from "react";
import { useLocation } from "react-router-dom";
import { useDimensions, usePageContent, useUnhead } from "../../Shared/Hooks";
import { PdfViewer } from "../../Components/PdfViewer";
import { Navigation } from "../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const PdfViewerPage = () => {
    useUnhead("PdfViewerPage");
    usePageContent(["navigation", "footer", "templates", "cookiesPrompt", "pagePdfViewer"], "PdfViewerPage");
    const media = useDimensions();

    const queryParam = useQuery();
    const name = queryParam.get("name") ?? "";

    return (
        <>
            <Navigation backNavigationOnly isAlwaysVisible={media.isMobile} />
            <main>
                <PdfViewer pdfFile={name} />
            </main>
        </>
    );
};
