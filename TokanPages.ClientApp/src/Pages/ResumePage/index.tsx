import React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { CustomBreadcrumb } from "../../Shared/Components";
import { Footer, Navigation } from "../../Components/Layout";

export const ResumePage = (): React.ReactElement => {
    const heading = useUnhead("ResumePage");
    useSnapshot();
    usePageContent(["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "pageResume"], "ResumePage");

    const state = useSelector((state: ApplicationState) => state);
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <CustomBreadcrumb isLoading={isLoading} />
                <div>EMPTY PAGE</div>
            </main>
            <Footer />
        </>
    );
}