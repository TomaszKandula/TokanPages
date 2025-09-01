import React from "react";
import { usePageContent, useQuery, useUnhead } from "../../Shared/Hooks";
import { PdfViewer } from "../../Components/PdfViewer";
import { Footer, Navigation } from "../../Components/Layout";

export const PdfViewerPage = () => {
    const heading = useUnhead("PdfViewerPage");
    usePageContent(["navigation", "layoutFooter", "templates", "cookiesPrompt", "pagePdfViewer"], "PdfViewerPage");

    const queryParam = useQuery();
    const name = queryParam.get("name") ?? "";

    return (
        <>
            <Navigation backNavigationOnly />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <PdfViewer pdfFile={name} />
            </main>
            <Footer />
        </>
    );
};
