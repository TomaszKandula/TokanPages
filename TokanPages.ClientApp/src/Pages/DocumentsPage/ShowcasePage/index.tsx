import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { TryPostStateSnapshot } from "../../../Shared/Services/SpaCaching";
import { Navigation, Footer } from "../../../Components/Layout";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../Shared/Components";

export const ShowcasePage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const showcase = state?.contentPageData?.components?.showcase;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "footer", "showcase"], "ShowcasePage"));
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.showcase.items ?? [];

    React.useEffect(() => {
        if (showcase?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb mt={96} mb={16} mr={40} ml={40} mtDivider={32} mbDivider={32} isLoading={isLoading} />
            <DocumentContentWrapper isLoading={isLoading} items={items} />
            <Footer />
        </>
    );
};
