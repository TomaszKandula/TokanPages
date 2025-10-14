import React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { usePageContent, useQuery, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { Breadcrumb } from "../../Shared/Components";
import { Footer, Navigation } from "../../Components/Layout";
import { Resume } from "../../Components/Resume";

export const ResumePage = (): React.ReactElement => {
    const heading = useUnhead("ResumePage");
    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "sectionTestimonials", "pageResume"],
        "ResumePage"
    );

    const queryParam = useQuery();
    const mode = queryParam.get("mode") ?? "";
    const isPrintable = mode === "printable";

    if (isPrintable) {
        return <Resume />;
    }

    const state = useSelector((state: ApplicationState) => state);
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <Breadcrumb isLoading={isLoading} />
                <Resume />
            </main>
            <Footer />
        </>
    );
};
