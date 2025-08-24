import React from "react";
import { usePageContent, useQuery, useUnhead } from "../../Shared/Hooks";
import { PdfViewer } from "../../Components/PdfViewer";
import { Footer, Navigation } from "../../Components/Layout";

export const PdfViewerPage = () => {
    useUnhead("PdfViewerPage");
    usePageContent(["navigation", "layoutFooter", "templates", "cookiesPrompt", "pagePdfViewer"], "PdfViewerPage");

    const queryParam = useQuery();
    const name = queryParam.get("name") ?? "";

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <PdfViewer pdfFile={name} />
            </main>
            <Footer />
        </>
    );
};
