import React from "react";
import { useLocation } from "react-router-dom";
import { usePageContent, useUnhead } from "../../Shared/Hooks";
import { PdfViewer } from "../../Components/PdfViewer";
import { Navigation } from "../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const PdfViewerPage = () => {
    useUnhead("PdfViewerPage");
    usePageContent(["navigation", "footer", "templates", "cookiesPrompt", "pagePdfViewer"], "PdfViewerPage");

    const queryParam = useQuery();
    const name = queryParam.get("name") ?? "";

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main className="mt-48">
                <PdfViewer pdfFile={name} />
            </main>
        </>
    );
};
