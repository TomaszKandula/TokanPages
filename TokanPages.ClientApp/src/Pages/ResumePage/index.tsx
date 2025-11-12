import React from "react";
import { useSelector } from "react-redux";
import { GET_DOCUMENTS_URL } from "../../Api";
import { ApplicationState } from "../../Store/Configuration";
import { usePageContent, useQuery, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { Breadcrumb } from "../../Shared/Components";
import { Footer, Navigation } from "../../Components/Layout";
import { Resume } from "../../Components/Resume";

const RESUME_NAME_FRAGMENT = "-resume-tom-kandula.pdf";

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

    const state = useSelector((state: ApplicationState) => state);
    const languageId = state.applicationLanguage.id;
    const data = state.contentPageData;
    const isLoading = data?.isLoading ?? false;

    const url = `${GET_DOCUMENTS_URL}/${languageId}${RESUME_NAME_FRAGMENT}`;

    if (isPrintable) {
        return <Resume />;
    }

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <Breadcrumb isLoading={isLoading} downloadUrl={url} />
                <Resume />
            </main>
            <Footer />
        </>
    );
};
