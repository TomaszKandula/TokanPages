import React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { Footer, Navigation } from "Components/Layout";

export const ResumePage = (): React.ReactElement => {
    const heading = useUnhead("ResumePage");
    useSnapshot();
    usePageContent(["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageResume"], "ResumePage");

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <div>TEST</div>
            </main>
            <Footer />
        </>
    );
}