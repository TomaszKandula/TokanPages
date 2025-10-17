import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { Breadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { usePageContent, useSnapshot, useUnhead } from "../../../../Shared/Hooks";
import { Navigation, Footer } from "../../../../Components/Layout";

export const GuitarPage = (): React.ReactElement => {
    const heading = useUnhead("GuitarPage");
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
                <h1 className="seo-only">{heading}</h1>
                <Breadcrumb isLoading={isLoading} />
                <DocumentContentWrapper isLoading={isLoading} items={items} />
            </main>
            <Footer />
        </>
    );
};
