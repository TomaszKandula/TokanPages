import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { usePageContent, useSnapshot, useUnhead } from "../../../../Shared/Hooks";
import { Navigation, Footer } from "../../../../Components/Layout";

export const PolicyPage = (): React.ReactElement => {
    const heading = useUnhead("PolicyPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "legalPolicy", "layoutFooter", "sectionCookiesPrompt"], "PolicyPage");

    const state = useSelector((state: ApplicationState) => state);
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;
    const items = data?.components.legalPolicy.items ?? [];

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <CustomBreadcrumb isLoading={isLoading} />
                <DocumentContentWrapper isLoading={isLoading} items={items} />
            </main>
            <Footer />
        </>
    );
};
