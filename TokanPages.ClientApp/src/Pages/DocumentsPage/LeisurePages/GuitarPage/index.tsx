import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { usePageContent, useSnapshot, useUnhead } from "../../../../Shared/Hooks";
import { Navigation, Footer } from "../../../../Components/Layout";

export const GuitarPage = (): React.ReactElement => {
    useUnhead("GuitarPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "layoutFooter", "sectionCookiesPrompt", "leisureGuitar"], "GuitarPage");

    const state = useSelector((state: ApplicationState) => state);
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;
    const items = data?.components.leisureGuitar.items ?? [];

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
