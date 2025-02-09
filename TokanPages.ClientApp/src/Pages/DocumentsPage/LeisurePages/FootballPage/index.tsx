import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentPageDataAction } from "../../../../Store/Actions";
import { TryPostStateSnapshot } from "../../../../Shared/Services/SpaCaching";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { Navigation, Footer } from "../../../../Components/Layout";
import { Cookies } from "../../../../Components/Cookies";

export const FootballPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const football = state?.contentPageData?.components?.football;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request([
            "navigation", 
            "footer", 
            "cookiesPrompt", 
            "football"
        ], "FootballPage"));
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.football.items ?? [];

    React.useEffect(() => {
        if (football?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb mt={96} mb={16} mr={40} ml={40} mtDivider={32} mbDivider={32} isLoading={isLoading} />
            <DocumentContentWrapper isLoading={isLoading} items={items} />
            <Cookies />
            <Footer />
        </>
    );
};
