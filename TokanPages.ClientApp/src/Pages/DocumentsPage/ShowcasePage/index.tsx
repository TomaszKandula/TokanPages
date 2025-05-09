import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../Shared/Components";
import { usePageContent, useSnapshot, useUnhead } from "../../../Shared/Hooks";
import { Navigation, Footer } from "../../../Components/Layout";

export const ShowcasePage = (): React.ReactElement => {
    useUnhead("ShowcasePage");
    useSnapshot();
    usePageContent(["layoutNavigation", "layoutFooter", "pageShowcase", "sectionCookiesPrompt"], "ShowcasePage");

    const state = useSelector((state: ApplicationState) => state);
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;
    const items = data?.components.pageShowcase.items ?? [];

    return (
        <>
            <Navigation />
            <main>
                <CustomBreadcrumb isLoading={isLoading} />
                <DocumentContentWrapper isLoading={isLoading} items={items} />
            </main>
            <Footer />
        </>
    );
};
