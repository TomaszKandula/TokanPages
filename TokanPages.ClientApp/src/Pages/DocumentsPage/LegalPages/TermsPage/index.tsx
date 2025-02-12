import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentPageDataAction } from "../../../../Store/Actions";
import { TryPostStateSnapshot } from "../../../../Shared/Services/SpaCaching";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { Navigation, Footer } from "../../../../Components/Layout";
import { Cookies } from "../../../../Components/Cookies";

export const TermsPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const terms = state?.contentPageData?.components?.terms;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request([
            "navigation", 
            "footer", 
            "terms",
            "cookiesPrompt", 
        ], "TermsPage"));
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.terms.items ?? [];

    React.useEffect(() => {
        if (terms?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb isLoading={isLoading} />
            <DocumentContentWrapper isLoading={isLoading} items={items} />
            <Cookies />
            <Footer />
        </>
    );
};
