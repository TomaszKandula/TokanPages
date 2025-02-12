import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentPageDataAction } from "../../../../Store/Actions";
import { TryPostStateSnapshot } from "../../../../Shared/Services/SpaCaching";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { Navigation, Footer } from "../../../../Components/Layout";
import { Cookies } from "../../../../Components/Cookies";

export const ElectronicsPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const electronics = state?.contentPageData?.components?.electronics;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request([
            "navigation", 
            "footer", 
            "cookiesPrompt", 
            "electronics"
        ], "ElectronicsPage"));
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.electronics.items ?? [];

    React.useEffect(() => {
        if (electronics?.language !== "") {
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
